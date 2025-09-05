// import React, { useEffect, useState } from "react";
// import { Box, CircularProgress, Grid } from "@mui/material";
// import {
//   getLastCreatedCourses,
//   type TeacherOwnCourse,
// } from "../../services/courseService";
// import BasicCard from "../../components/common/BasicCard/BasicCard";
// import { useNavigate } from "react-router-dom";
// import { motion } from "framer-motion";
// import InfiniteScroll from "react-infinite-scroll-component";

// const PAGE_SIZE = 10;

// const Courses = () => {
//   const [courses, setCourses] = useState<TeacherOwnCourse[]>([]);
//   const [page, setPage] = useState(1);
//   const [hasMore, setHasMore] = useState(true);
//   const navigate = useNavigate();

//   const isNew = (dateCreated: string): boolean => {
//     const createdDate = new Date(dateCreated);
//     const now = new Date();
//     const daysDiff = (now.getTime() - createdDate.getTime()) / (1000 * 60 * 60 * 24);
//     return daysDiff <= 7;
//   };

//   const loadCourses = async () => {
//     try {
//       const data = await getLastCreatedCourses(page, PAGE_SIZE);
//       console.log("courses:",data);
//       setCourses((prev) => [...prev, ...data]);
//       setHasMore(data.length === PAGE_SIZE);
//       setPage((prev) => prev + 1);
//     } catch (error) {
//       console.error("Error loading courses:", error);
//     }
//   };

//   useEffect(() => {
//     loadCourses(); // first load
//   }, []);

//   return (
//     <motion.div
//       initial={{ opacity: 0, y: 10 }}
//       animate={{ opacity: 1, y: 0 }}
//       exit={{ opacity: 0, y: 10 }}
//       transition={{ duration: 0.3, ease: "easeInOut" }}
//     >
//       <Box sx={{ flexGrow: 1, mt: 1 }}>
//         <InfiniteScroll
//           dataLength={courses.length}
//           next={loadCourses}
//           hasMore={hasMore}
//           loader={
//             <Box sx={{ display: "flex", justifyContent: "center", py: 2 }}>
//               <CircularProgress size={24} />
//             </Box>
//           }
//           endMessage={
//             <Box sx={{ textAlign: "center", py: 2 }}>
//               All courses loaded 🎉
//             </Box>
//           }
//         >
//           <Grid container spacing={2}>
//             {courses.map((course, index) => {
//               const size =
//                 index === 0
//                   ? { xs: 12, md: 12, lg: 8 }
//                   : index === 1
//                   ? { xs: 12, md: 6, lg: 4 }
//                   : index === 2
//                   ? { xs: 12, md: 6, lg: 4 }
//                   : index === 3
//                   ? { xs: 12, md: 12, lg: 8 }
//                   : { xs: 12, sm: 12, md: 6, lg: 4 };

//               return (
//                 <Grid key={course.id} size={size}>
//                   <BasicCard
//                     title={course.title}
//                     description={course.description}
//                     modules={course.lessonsCount}
//                     difficulty={course.difficulty}
//                     category={course.categoryName}
//                     isNew={isNew(course.dateModified)}
//                     isPro={false}
//                     avatars={course.authorAvatarUrl ? [course.authorAvatarUrl] : []}
//                     onClick={() => navigate(`/app/course/${course.id}`)}
//                   />
//                 </Grid>
//               );
//             })}
//           </Grid>
//         </InfiniteScroll>
//       </Box>
//     </motion.div>
//   );
// };

// export default Courses;




////////////////////////////////

// import React, { useMemo, useState } from "react";
// import { Box, Tabs, Tab, useTheme, } from "@mui/material";
// import CoursesFiltersPanel from "./CourseFiltersPanel";
// import { alpha } from "@mui/material/styles";
// import useMediaQuery from "@mui/material/useMediaQuery";

// const tabLabels = [
//   { label: "Browse", count: 80 },
//   { label: "Started", count: 3 },
//   { label: "Completed", count: 1 },
// ];

// const CoursesTabs: React.FC = () => {
//   const [tab, setTab] = useState(0);

//   const theme = useTheme();

//   const isXS = useMediaQuery("(max-width:393px)");
//   const isSM = useMediaQuery("(min-width:393px) and (max-width:599px)");

//   const topOffset = useMemo(() => {
//     if (isXS) return 55;
//     if (isSM) return 47;
//     return 63;
//   }, [isXS, isSM]);

//   const handleChange = (_: React.SyntheticEvent, newValue: number) => {
//     setTab(newValue);
//   };


//   return (
//     <Box
//       sx={{
//         position: "sticky",
//         top: topOffset,
//         zIndex: 1500,
//         backgroundColor: alpha(theme.palette.background.default, 0.8),
//         backdropFilter: "blur(6px)", // для эффекта размытияs
//       }}
//     >
//       <Tabs
//         value={tab}
//         onChange={handleChange}
//         sx={{
//           "& .MuiTabs-indicator": {
//             backgroundColor: theme.palette.button.main,
//           },
//         }}
//       >
//         {tabLabels.map((tabItem, index) => (
//           <Tab
//             key={index}
//             label={`${tabItem.label} (${tabItem.count})`}
//             sx={{
//               textTransform: "none",
//               color: theme.palette.text.primary,
//               "&:hover": {
//                 color: theme.palette.text.secondary,
//               },
//               "&.Mui-selected": {
//                 color: theme.palette.text.secondary,
//               },
//             }}
//           />
//         ))}
//       </Tabs>

//       <CoursesFiltersPanel />
      
//     </Box>

//   );
// };

// export default CoursesTabs;
