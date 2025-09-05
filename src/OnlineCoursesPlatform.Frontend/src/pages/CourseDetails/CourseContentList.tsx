import React, { useEffect, useState } from "react";
import { Backdrop, Box, CircularProgress } from "@mui/material";
import LessonCard from "../../components/common/LessonCard/LessonCard";
import { useParams } from "react-router-dom";
import LessonDetailsPage from "../LessonDetails/LessonDetailsPage";
import EditLessonPage from "../Teacher/EditLesson/EditLessonPage";
import type { LessonList } from "../../services/lessonService";
import { AnimatePresence, motion } from "framer-motion";

interface CourseContentListProps {
  courseId: number;
  lessons: LessonList[];
  lessonsLoading: boolean;
  onLessonDelete: (orderNumber: number) => void;
  authorId: number;
}

const CourseContentList: React.FC<CourseContentListProps> = ({
  courseId,
  lessons,
  lessonsLoading,
  onLessonDelete,
  authorId,
}) => {
  const [detailsLoading, setDetailsLoading] = useState(false);
  const { orderNumber } = useParams();
  const selectedOrderNumber = orderNumber ? Number(orderNumber) : null;
  const isEdit = window.location.pathname.includes("/edit");

  if (lessonsLoading) {
    return (
      <Backdrop open sx={{ color: "#fff", zIndex: "1700" }}>
        <CircularProgress color="inherit" />
      </Backdrop>
    );
  }

  useEffect(() => {
    if (selectedOrderNumber !== null) {
      // Сначала ждем, пока анимация начнется
      const timer = setTimeout(() => {
        document.documentElement.style.scrollbarGutter = "unset"; 
      }, 100); 

      // блокируем прокрутку фона
      document.body.style.overflow = "hidden";

      return () => {
        clearTimeout(timer);
        document.body.style.overflow = "auto";
        document.documentElement.style.scrollbarGutter = "stable"; 
      };
    }
  }, [selectedOrderNumber]);

  return (
    <Box>
      {lessons.map((lesson) => {
        const isSelected = selectedOrderNumber === lesson.orderNumber;

        return (
          <Box key={`lesson-${lesson.id}`}>
            <AnimatePresence mode="wait">
              <motion.div
                layout
                initial={{ opacity: 0, y: 10 }}
                animate={{ opacity: 1, y: 0 }}
                exit={{ opacity: 0, y: 10 }}
                transition={{ duration: 0.3, ease: "easeInOut" }}
              >
                <LessonCard
                  courseId={courseId}
                  title={lesson.title}
                  onEdit={() => console.log(`Edit lesson ${lesson.id}`)}
                  onDelete={onLessonDelete}
                  orderNumber={lesson.orderNumber}
                  selectedOrderNumber={selectedOrderNumber}
                  authorId={authorId}
                />
              </motion.div>
            </AnimatePresence>

            <AnimatePresence mode="wait">
              {isSelected && (
                <motion.div
                  key={`details-${lesson.id}-${isEdit ? "edit" : "view"}`}
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
                    zIndex: 2000,
                    backgroundColor: "rgba(0, 0, 0, 0.9)",
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
                    }}
                  >
                    <Box
                      sx={{
                        height: "100%",
                        overflowY: "auto",
                        p: { xs: 2, sm: 4 },
                      }}
                    >
                      {isEdit ? <EditLessonPage /> : <LessonDetailsPage />}
                    </Box>
                  </motion.div>
                </motion.div>
              )}
            </AnimatePresence>
          </Box>
        );
      })}
    </Box>
  );
};

export default CourseContentList;
