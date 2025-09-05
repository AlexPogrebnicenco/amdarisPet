import { useEffect, useState } from "react";
import {
  Box,
  CircularProgress,
  Grid,
  Typography,
  useTheme,
} from "@mui/material";
import { getTeacherOwnCourses } from "../../../services/courseService";
import BasicCard from "../../../components/common/BasicCard/BasicCard";
import { useNavigate, useOutletContext } from "react-router-dom";
import { motion } from "framer-motion";
import InfiniteScroll from "react-infinite-scroll-component";
import type { TeacherOwnCourse } from "../../../interfaces/CourseDto";
import SearchOffIcon from "@mui/icons-material/SearchOff";
import CelebrationTwoToneIcon from "@mui/icons-material/CelebrationTwoTone";

const PAGE_SIZE = 10;

const MyCoursesTab = () => {
  const theme = useTheme();
  const navigate = useNavigate();

  const { sort, tag, searchQuery } = useOutletContext<{
    sort: string;
    tag: string | null;
    searchQuery: string;
  }>();

  const [courses, setCourses] = useState<TeacherOwnCourse[]>([]);
  const [page, setPage] = useState(1);
  const [hasMore, setHasMore] = useState(true);
  const [isLoading, setIsLoading] = useState(false);

  const isNew = (dateCreated: string): boolean => {
    const createdDate = new Date(dateCreated);
    const now = new Date();
    const daysDiff =
      (now.getTime() - createdDate.getTime()) / (1000 * 60 * 60 * 24);
    return daysDiff <= 7;
  };

  const loadCourses = async (currentPage: number) => {
    setIsLoading(true);
    try {
      const data = await getTeacherOwnCourses({
        page: currentPage,
        pageSize: PAGE_SIZE,
        sort,
        tag,
        search: searchQuery,
      });
      console.log("Data:", data),
      setCourses((prev) =>
        currentPage === 1 ? data.items : [...prev, ...data.items]
      );
      setHasMore(data.items.length === PAGE_SIZE);
    } catch (error) {
      console.error("Error loading enrolled courses:", error);
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    setCourses([]);
    setPage(1);
    setHasMore(true);
    loadCourses(1);
  }, [sort, tag, searchQuery]);

  const fetchNext = () => {
    const nextPage = page + 1;
    setPage(nextPage);
    loadCourses(nextPage);
  };

  return (
    <motion.div
      initial={{ opacity: 0, y: 10 }}
      animate={{ opacity: 1, y: 0 }}
      exit={{ opacity: 0, y: 10 }}
      transition={{ duration: 0.3, ease: "easeInOut" }}
    >
      <Box sx={{ flexGrow: 1, mt: 1 }}>
        {isLoading && page === 1 ? (
          <Box sx={{ display: "flex", justifyContent: "center", py: 4 }}>
            <CircularProgress size={32} />
          </Box>
        ) : courses.length === 0 ? (
          <Box
            sx={{
              height: "60vh",
              display: "flex",
              flexDirection: "column",
              alignItems: "center",
              justifyContent: "center",
              textAlign: "center",
              gap: 2,
            }}
          >
            <SearchOffIcon
              sx={{
                fontSize: 64,
                color: theme.palette.button.main,
                animation: "pulse 1.5s infinite ease-in-out",
                "@keyframes pulse": {
                  "0%": { transform: "scale(1)" },
                  "50%": { transform: "scale(1.1)" },
                  "100%": { transform: "scale(1)" },
                },
              }}
            />
            <Typography variant="h6" sx={{ mt: 2 }}>
              No results found
            </Typography>
            <Typography variant="body2">Create your first course</Typography>
          </Box>
        ) : (
          <InfiniteScroll
            dataLength={courses.length}
            next={fetchNext}
            hasMore={hasMore}
            loader={
              <Box sx={{ display: "flex", justifyContent: "center", py: 2 }}>
                <CircularProgress size={24} />
              </Box>
            }
            endMessage={
              <Box
                sx={{
                  display: "flex",
                  alignItems: "center",
                  justifyContent: "center",
                  gap: 1,
                  py: 2,
                }}
              >
                <Typography variant="body1">All courses loaded</Typography>
                <CelebrationTwoToneIcon sx={{ color: "#FFD600", mb: 1 }} />
              </Box>
            }
          >
            <Grid container spacing={2}>
              {courses.map((course, index) => {
                const size =
                  index === 0
                    ? { xs: 12, md: 12, lg: 8 }
                    : index === 1
                    ? { xs: 12, md: 6, lg: 4 }
                    : index === 2
                    ? { xs: 12, md: 6, lg: 4 }
                    : index === 3
                    ? { xs: 12, md: 12, lg: 8 }
                    : { xs: 12, sm: 12, md: 6, lg: 4 };

                return (
                  <Grid key={course.id} size={size}>
                    <BasicCard
                      title={course.title}
                      description={course.description}
                      modules={course.lessonsCount}
                      difficulty={course.difficulty}
                      category={course.categoryName}
                      isNew={isNew(course.dateModified)}
                      isPro={false}
                      avatars={
                        course.authorAvatarUrl ? [course.authorAvatarUrl] : []
                      }
                      onClick={() => navigate(`/app/course/${course.id}`)}
                    />
                  </Grid>
                );
              })}
            </Grid>
          </InfiniteScroll>
        )}
      </Box>
    </motion.div>
  );
};

export default MyCoursesTab;
