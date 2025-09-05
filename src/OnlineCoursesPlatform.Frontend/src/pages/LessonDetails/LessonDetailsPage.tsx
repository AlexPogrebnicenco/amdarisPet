import { useNavigate, useParams } from "react-router-dom";
import {
  Backdrop,
  Box,
  CircularProgress,
  Divider,
  IconButton,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  Paper,
  Typography,
  useTheme,
} from "@mui/material";
import { useEffect, useState } from "react";
import {
  getLessonByOrderNumber,
  type Lesson,
} from "../../services/lessonService";
import VideoPlayerList from "../../components/common/VideoPlayer/VideoPlayerList";
import ExpandableTextLesson from "../../components/common/ExpandableText/ExpandableTextLesson";
import CustomLink from "../../components/common/CustomLink/CustomLink";
import FiberManualRecordTwoToneIcon from "@mui/icons-material/FiberManualRecordTwoTone";
import CancelTwoToneIcon from "@mui/icons-material/CancelTwoTone";
import Markdown from "@uiw/react-markdown-preview";

const LessonDetailsPage = () => {
  const theme = useTheme();
  const { courseId, orderNumber } = useParams();
  const [lesson, setLesson] = useState<Lesson | null>(null);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchLesson = async () => {
      if (!courseId || !orderNumber) return;

      try {
        setLoading(true);
        //  await new Promise((resolve) => setTimeout(resolve, 3000));
        const data = await getLessonByOrderNumber(
          Number(courseId),
          Number(orderNumber)
        );
        console.log("Fetched lesson:", data);
        setLesson(data);
      } catch (error) {
        console.error("Error fetching lesson:", error);
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
  if (!lesson) {
    return (
      <Backdrop
        open={true}
        sx={{
          color: "#fff",
          zIndex: "1700",
          backgroundColor: "transparent",
        }}
      >
        <Typography variant="body1">Loading lesson...</Typography>
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

        <Typography
          variant="h4"
          gutterBottom
          sx={{
            textAlign: "center",
            color: theme.palette.text.secondary,
            border: "3px solid transparent",
            borderRadius: 2,
            background: `
                  linear-gradient(rgba(1, 11, 17, 0.1), rgba(2, 28, 42, 0.1)) padding-box,
                  linear-gradient(135deg, rgba(2, 28, 42, 0.1), #033d5b, rgba(2, 28, 42, 0.1)) border-box
                `,
            backgroundOrigin: "border-box",
            backgroundClip: "padding-box, border-box",
            p: 2,
          }}
        >
          {lesson.title}
        </Typography>

        {/* Description block  */}
        <Divider sx={{ my: 2 }}>
          <Typography variant="subtitle1" gutterBottom>
            Description:
          </Typography>
        </Divider>
        <Box sx={{textAlign:"center"}}>
        <ExpandableTextLesson text={lesson.description} />
        </Box>

        {/* Video block  */}
        <Divider sx={{ my: 2 }}>
          <Typography variant="subtitle1" gutterBottom>
            Videos:
          </Typography>
        </Divider>
        <VideoPlayerList videoUrls={lesson.videoUrls || []} />

        {/* Content block */}
        <Divider sx={{ my: 2 }}>
          <Typography variant="subtitle1" gutterBottom>
            Content:
          </Typography>
        </Divider>

        {lesson.content ? (
          <Box
            data-color-mode="dark" 
            style={{ width: "100%" }}
          >
            <Markdown
              source={lesson.content}
              style={{
                padding: "1rem",
                backgroundColor: "transparent",
                color: theme.palette.text.primary,
              }}
            />
          </Box>
        ) : (
          <Typography variant="body1" gutterBottom>
            No content provided.
          </Typography>
        )}

        {/* Resources block */}
        <Divider sx={{ my: 2 }}>
          <Typography variant="subtitle1" gutterBottom>
            Resources:
          </Typography>
        </Divider>

        {lesson.resources && lesson.resources.length > 0 ? (
          <List>
            {lesson.resources.map((resource, index) => (
              <ListItem key={index} sx={{ pl: 0 }}>
                <ListItemIcon
                  sx={{ minWidth: "24px", color: "text.secondary" }}
                >
                  <FiberManualRecordTwoToneIcon
                    fontSize="small"
                    sx={{ color: theme.palette.button.main }}
                  />
                </ListItemIcon>
                <ListItemText
                  primary={<CustomLink to={resource}>{resource}</CustomLink>}
                />
              </ListItem>
            ))}
          </List>
        ) : (
          <Typography variant="body2">No resources provided.</Typography>
        )}
      </Paper>
    </Box>
  );
};

export default LessonDetailsPage;
