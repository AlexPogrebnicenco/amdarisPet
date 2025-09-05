import { Box, IconButton, Paper, useTheme } from "@mui/material";
import React from "react";
import CancelTwoToneIcon from "@mui/icons-material/CancelTwoTone";
import EditCourseForm from "./EditCourseForm";
import type { CourseDetails } from "../../../interfaces/CourseDto";

interface EditCoursePageProps {
  onClose: () => void;
  course: CourseDetails;
}

const EditCoursePage: React.FC<EditCoursePageProps> = ({ onClose, course }) => {
  const theme = useTheme();
  return (
    <Box
      sx={{
        minHeight: "100%",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        zIndex: 3000,
      }}
    >
      <Paper
        elevation={6}
        sx={{
          position: "relative",
          p: { xs: 3, sm: 4 },
          width: "100%",
          backgroundColor: theme.palette.background.paper,
          borderRadius: 3,
          boxShadow: "0 8px 24px rgba(0, 0, 0, 0.3)",
          zIndex: 3000,
        }}
      >
        <IconButton
          onClick={onClose}
          aria-label="Close lesson"
          sx={{
            position: "absolute",
            top: 16,
            right: 16,
            zIndex: 10,
            transition: "background-color 0.3s, color 0.3s",
            "&:hover": {
              backgroundColor: "#38424D",
              "& svg": { color: "#FF4C4C" },
            },
          }}
        >
          <CancelTwoToneIcon sx={{ color: "#A5B1C2", fontSize: "35px" }} />
        </IconButton>

        <EditCourseForm  course={course}/>
      </Paper>
    </Box>
  );
};

export default EditCoursePage;
