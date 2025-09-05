import { useEffect, useState } from "react";
import {
  Box,
  IconButton,
  Paper,
  useTheme,
  CircularProgress,
  Backdrop,
  Typography,
} from "@mui/material";
import { useNavigate, useParams } from "react-router-dom";
import CancelTwoToneIcon from "@mui/icons-material/CancelTwoTone";
import EditLessonForm from "./EditLessonForm";
import {
  getLessonByOrderNumber,
  type Lesson,
} from "../../../services/lessonService";
import { toast } from "react-toastify";

const EditLessonPage = () => {
  const theme = useTheme();
  const navigate = useNavigate();
  const { courseId, orderNumber } = useParams();

  const [lesson, setLesson] = useState<Lesson | null>(null);
  const [loading, setLoading] = useState(true);
  const [notFound, setNotFound] = useState(false);

  useEffect(() => {
    const fetchLesson = async () => {
      if (!courseId || !orderNumber) return;

      try {
        const data = await getLessonByOrderNumber(+courseId, +orderNumber);
        if (!data) {
          setNotFound(true);
        } else {
          setLesson(data);
          console.log("data for editLessonForm", data);
        }
      } catch (error: any) {
        if (error?.response?.status === 404) {
          setNotFound(true);
        } else {
          toast.error("Something went wrong.");
          console.error(error);
        }
      } finally {
        setLoading(false);
      }
    };

    fetchLesson();
  }, [courseId, orderNumber]);

  if (loading) {
    return (
      <Backdrop
        open={true}
        sx={{
          color: "#fff",
          zIndex: "1700",
          backgroundColor: "transparent",
        }}
      >
        <CircularProgress sx={{ color: "#0369A1" }} />
      </Backdrop>
    );
  }
  if (notFound) {
    return (
      <Backdrop
        open={true}
        sx={{
          color: "#fff",
          zIndex: "1700",
          backgroundColor: "transparent",
        }}
      >
        <Typography variant="body1"> Lesson not found.</Typography>
      </Backdrop>
    );
  }

  return (
    <Box
      sx={{
        minHeight: "100%",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
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
        }}
      >
        <IconButton
          onClick={() => navigate(`/app/course/${courseId}`)}
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

        {lesson && <EditLessonForm lesson={lesson} />}
      </Paper>
    </Box>
  );
};

export default EditLessonPage;
