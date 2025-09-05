import { useEffect, useState } from "react";
import { Outlet, useParams, useSearchParams } from "react-router-dom";
import {
  Backdrop,
  Box,
  CircularProgress,
  Typography,
  useTheme,
} from "@mui/material";
import { getCourseById } from "../../services/courseService";
import RoleBasedRender from "../../components/common/RoleBasedRender/RoleBasedRender";
import { useAuth } from "../../context/AuthContext";
import CourseContentList from "./CourseContentList";
import OneEntityTabs from "../../components/common/UniversalTabs/OneEntityTabs";
import ExpandableText from "../../components/common/ExpandableText/ExpandableText";
import CourseHeader from "./CourseHeader";
import AddLessonTab from "../Teacher/AddLesson/AddLessonTab";
import {
  getLessonsByCourseId,
  type LessonList,
} from "../../services/lessonService";
import { toast } from "react-toastify";
import { motion } from "framer-motion";
import type { CourseDetails } from "../../interfaces/CourseDto";
import CourseAbout from "./CourseAbout";

const CourseDetailsPage = () => {
  const theme = useTheme();
  const { courseId } = useParams<{ courseId: string }>();
  const { role, userId } = useAuth();

  const [searchParams, setSearchParams] = useSearchParams();
  const tab = searchParams.get("tab") || "content";

  const [course, setCourse] = useState<CourseDetails | null>(null);
  const [loading, setLoading] = useState(true);
  const [lessons, setLessons] = useState<LessonList[]>([]);
  const [lessonsLoading, setLessonsLoading] = useState(true);

  useEffect(() => {
    const fetchCourse = async () => {
      try {
        const data = await getCourseById(Number(courseId));
        console.log("Course details:", data);
        setCourse(data);
      } catch (error) {
        console.error("Error fetching course:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchCourse();
  }, [courseId]);

  useEffect(() => {
    if (!course) return;

    const fetchLessons = async () => {
      try {
        setLessonsLoading(true);
        const data = await getLessonsByCourseId(course.id);
        setLessons(data);
      } catch (error) {
        console.error("Error fetching lessons:", error);
      } finally {
        setLessonsLoading(false);
      }
    };

    fetchLessons();
  }, [course?.id]);

  if (loading)
    return (
      <Backdrop
        open={true}
        sx={{
          backgroundColor: "transparent",
          zIndex: "1700",
          pointerEvents: "none",
        }}
      >
        <CircularProgress color="inherit" />
      </Backdrop>
    );

  if (!course) return <div>Course not found.</div>;

  const courseTabs = [
    { label: "Content", value: "content" },
    { label: "About", value: "about" },
  ];

  if (role === "Admin" || (role === "Teacher" && course?.authorId === userId)) {
    courseTabs.push({ label: "Add lesson", value: "add-lesson" });
  }

  // Переход на вкладку Content и перезагрузка уроков
  const handleLessonCreated = async () => {
    setSearchParams({ tab: "content" });
    try {
      const updatedLessons = await getLessonsByCourseId(course.id);
      setLessons(updatedLessons);
    } catch (error) {
      console.error("Error refreshing lessons:", error);
    }
  };

  const handleLessonDelete = (orderNumber: number) => {
    setLessons((prevLessons) =>
      prevLessons.filter((lesson) => lesson.orderNumber !== orderNumber)
    );
    toast.success("Lesson deleted successfully!");
  };

  return (
    <motion.div
      initial={{ opacity: 0, y: 10 }}
      animate={{ opacity: 1, y: 0 }}
      exit={{ opacity: 0, y: 10 }}
      transition={{ duration: 0.3, ease: "easeInOut" }}
    >
      <Box>
        <Box
          sx={{
            position: "relative",
            zIndex: 1450,
            backgroundColor: "#161A1D",
          }}
        >
          {/* Course Header */}
          <Box sx={{ display: "flex", alignItems: "center", gap: 2, mb: 2 }}>
            <Typography
              variant="h4"
              sx={{ color: theme.palette.text.secondary }}
            >
              {course.title}
            </Typography>
            {courseId && <CourseHeader courseId={Number(courseId)} authorId={course.authorId} course={course} />}
          </Box>

          <ExpandableText text={course.description} />
        </Box>

        {/* Tabs */}
        <OneEntityTabs tabs={courseTabs} />
        {/* Tab content */}
        {tab === "content" && (
          <Box sx={{ mt: 2 }}>
            {lessons.length === 0 ? (
              <Typography
                variant="body1"
                sx={{
                  color: theme.palette.text.primary,
                  textAlign: "center",
                  mt: 4,
                }}
              >
                Please use the "Add lesson" tab to add the first lesson.
              </Typography>
            ) : (
              <CourseContentList
                courseId={course.id}
                lessons={lessons}
                lessonsLoading={lessonsLoading}
                onLessonDelete={handleLessonDelete}
                authorId={course.authorId}
              />
            )}
          </Box>
        )}

        {tab === "about" && (
          <motion.div
            initial={{ opacity: 0, y: 10 }}
            animate={{ opacity: 1, y: 0 }}
            exit={{ opacity: 0, y: 10 }}
            transition={{ duration: 0.3, ease: "easeInOut" }}
          >
            <CourseAbout about={course.about} />
          </motion.div>
        )}

        {tab === "add-lesson" && (
          <RoleBasedRender
            allowedRoles={["Teacher", "Admin"]}
            authorId={course.authorId}
          >
            <motion.div
              initial={{ opacity: 0, y: 10 }}
              animate={{ opacity: 1, y: 0 }}
              exit={{ opacity: 0, y: 10 }}
              transition={{ duration: 0.3, ease: "easeInOut" }}
            >
              <AddLessonTab
                onLessonCreated={handleLessonCreated}
                existingLessons={lessons}
                courseId={Number(courseId)}
                courseTitle={course.title}
              />
            </motion.div>
          </RoleBasedRender>
        )}
      </Box>
    </motion.div>
  );
};

export default CourseDetailsPage;
