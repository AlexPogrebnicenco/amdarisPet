import React, { useEffect, useState } from "react";
import {
  Box,
  IconButton,
  Tooltip,
  Typography,
  useTheme,
  Stack,
  CircularProgress,
} from "@mui/material";
import BookmarkTwoToneIcon from "@mui/icons-material/BookmarkTwoTone";
import BookmarkIcon from "@mui/icons-material/Bookmark";
import {
  enrollInCourse,
  isEnrolledInCourse,
  unenrollFromCourse,
} from "../../services/enrollmentService";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import { deleteCourse } from "../../services/courseService";
import { useNavigate } from "react-router-dom";
import RoleBasedRender from "../../components/common/RoleBasedRender/RoleBasedRender";
import EditCoursePage from "../Teacher/EditCourse/EditCoursePage";
import { AnimatePresence, motion } from "framer-motion";
import { createPortal } from "react-dom";
import type { CourseDetails } from "../../interfaces/CourseDto";
import { toast } from "react-toastify";

interface CourseHeaderProps {
  courseId: number;
  authorId: number;
  course: CourseDetails;
}

const CourseHeader: React.FC<CourseHeaderProps> = ({
  courseId,
  authorId,
  course,
}) => {
  const theme = useTheme();
  const navigate = useNavigate();

  const [isEnrolled, setIsEnrolled] = useState<boolean | null>(null);
  const [loading, setLoading] = useState(false);
  const [isEditOpen, setIsEditOpen] = useState(false);

  useEffect(() => {
    const checkEnrollment = async () => {
      try {
        const result = await isEnrolledInCourse(courseId);
        setIsEnrolled(result);
      } catch (err) {
        console.error("Error checking enrollment:", err);
      }
    };

    checkEnrollment();
  }, [courseId]);

  useEffect(() => {
    if (isEditOpen) {
      const timer = setTimeout(() => {
        document.documentElement.style.scrollbarGutter = "unset";
      }, 100);

      document.body.style.overflow = "hidden";

      return () => {
        clearTimeout(timer);
        document.body.style.overflow = "auto";
        document.documentElement.style.scrollbarGutter = "stable";
      };
    }
  }, [isEditOpen]);

  const toggleFollow = async () => {
    if (isEnrolled === null) return;
    setLoading(true);
    try {
      if (isEnrolled) {
        await unenrollFromCourse(courseId);
        setIsEnrolled(false);
      } else {
        await enrollInCourse(courseId);
        setIsEnrolled(true);
      }
    } catch (err) {
      console.error("Follow/unfollow failed:", err);
    }
    setLoading(false);
  };

  return (
    <Box sx={{ display: "flex", alignItems: "center" }}>
      <Tooltip title="Follow to get quick access to this in sidebar.">
        <span>
          <IconButton
            onClick={toggleFollow}
            disabled={loading || isEnrolled === null}
            sx={{
              display: "flex",
              alignItems: "center",
              color: theme.palette.button.main,
              border: "1px solid #33363E",
              borderRadius: 2,
              py: 0.5,
              px: 1,
              transition: "background-color 0.3s, color 0.3s",
              "&:hover": {
                backgroundColor: "#38424D",
                color: "#1E90FF",
              },
            }}
          >
            <Stack direction="row" alignItems="center" spacing={1}>
              {loading ? (
                <CircularProgress size={20} />
              ) : isEnrolled ? (
                <BookmarkIcon />
              ) : (
                <BookmarkTwoToneIcon />
              )}
              <Typography sx={{ fontSize: "1rem" }}>
                {isEnrolled ? "UNFOLLOW" : "FOLLOW"}
              </Typography>
            </Stack>
          </IconButton>
        </span>
      </Tooltip>

      <RoleBasedRender allowedRoles={["Teacher", "Admin"]} authorId={authorId}>
        <Stack direction="row" spacing={1} ml={2}>
          {/* Edit course button */}
          <IconButton
            onClick={() => {
              setIsEditOpen(true);
            }}
            sx={{
              transition: "background-color 0.3s, color 0.3s",
              "&:hover": {
                backgroundColor: "#38424D",
                "& svg": { color: "#FFD700" },
              },
            }}
          >
            <EditIcon sx={{ color: theme.palette.text.primary }} />
          </IconButton>

          {/* Delete course button */}
          <IconButton
            onClick={async () => {
              if (
                !window.confirm("Are you sure you want to delete this course?")
              )
                return;
              try {
                await deleteCourse(courseId);
                toast.success("Course deleted successfully.");
                navigate("/app");
              } catch (error) {
                toast.error("Failed to delete the course.");
                console.error("Error deleting course:", error);
              }
            }}
            sx={{
              transition: "background-color 0.3s, color 0.3s",
              "&:hover": {
                backgroundColor: "#38424D",
                "& svg": { color: "#FF4C4C" },
              },
            }}
          >
            <DeleteIcon sx={{ color: theme.palette.text.primary }} />
          </IconButton>
        </Stack>
      </RoleBasedRender>

      {/* 🟢 Edit Modal */}
      {isEditOpen &&
        createPortal(
          <AnimatePresence mode="wait">
            <motion.div
              key="edit-course"
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              exit={{ opacity: 0 }}
              transition={{ duration: 0.3 }}
              style={{
                position: "fixed",
                top: 0,
                left: 0,
                right: 0,
                bottom: 0,
                zIndex: 4000,
                backgroundColor: "rgba(0, 0, 0, 0.9)",
                overflowY: "auto",
              }}
            >
              <motion.div
                initial={{ x: "100%", opacity: 0 }}
                animate={{ x: 0, opacity: 1 }}
                exit={{ x: "100%", opacity: 0 }}
                transition={{ duration: 0.3, ease: "easeInOut" }}
                style={{
                  height: "100%",
                  overflowY: "auto",
                  padding: "2rem",
                  zIndex: 4000,
                }}
                onClick={(e) => e.stopPropagation()}
              >
                <EditCoursePage
                  course={course}
                  onClose={() => setIsEditOpen(false)}
                />
              </motion.div>
            </motion.div>
          </AnimatePresence>,
          document.body
        )}
    </Box>
  );
};

export default CourseHeader;
