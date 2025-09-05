import { useEffect, useState } from "react";
import {
  Box,
  CircularProgress,
  Grid,
  Typography,
  useTheme,
} from "@mui/material";
import {
  getSortedCourses,
  getCoursesByTags,
  searchCoursesByTitle,
} from "../../services/courseService";
import BasicCard from "../../components/common/BasicCard/BasicCard";
import { useNavigate, useOutletContext } from "react-router-dom";
import { motion } from "framer-motion";
import InfiniteScroll from "react-infinite-scroll-component";
import type { PublicCourse } from "../../interfaces/CourseDto";
import SearchOffIcon from "@mui/icons-material/SearchOff";
import CelebrationTwoToneIcon from "@mui/icons-material/CelebrationTwoTone";

const PAGE_SIZE = 10;

const BrowseCoursesTab = () => {
  const { sort, tag, searchQuery } = useOutletContext<{
    sort: string;
    tag: string | null;
    searchQuery: string;
  }>();
  const theme = useTheme();
  const [courses, setCourses] = useState<PublicCourse[]>([]);
  const [page, setPage] = useState(1);
  const [hasMore, setHasMore] = useState(true);
  const [isLoading, setIsLoading] = useState(false);
  const navigate = useNavigate();

  const isNew = (dateCreated: string): boolean => {
    const createdDate = new Date(dateCreated);
    const now = new Date();
    const daysDiff =
      (now.getTime() - createdDate.getTime()) / (1000 * 60 * 60 * 24);
    return daysDiff <= 7;
  };

  const loadCourses = async (currentPage: number) => {
    try {
      setIsLoading(true);
      // await new Promise((res) => setTimeout(res, 5000));
      let data: PublicCourse[] = [];

      if (searchQuery.length >= 3) {
        data = await searchCoursesByTitle(searchQuery, currentPage, PAGE_SIZE);
      } else if (tag) {
        data = await getCoursesByTags([tag], currentPage, PAGE_SIZE);
      } else {
        data = await getSortedCourses(sort, currentPage, PAGE_SIZE);
      }

      setCourses((prev) => (currentPage === 1 ? data : [...prev, ...data]));
      setHasMore(data.length === PAGE_SIZE);
      console.log("Data:", data);
    } catch (error) {
      console.error("Error loading courses:", error);
    } finally {
      setIsLoading(false);
    }
  };

  // когда sort меняется — сбрасываем
  useEffect(() => {
    setCourses([]);
    setPage(1);
    setHasMore(true);
    loadCourses(1);
  }, [sort, tag, searchQuery]);

  // догружаем по страницам
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
              textAlign: "center",
              py: 5,
              height: "100%",
              display: "flex",
              justifyContent: "center",
              alignItems: "center",
              flexDirection: "column",
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
            <Typography variant="body2">
              Adjust your filters or try a different search
            </Typography>
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

export default BrowseCoursesTab;
