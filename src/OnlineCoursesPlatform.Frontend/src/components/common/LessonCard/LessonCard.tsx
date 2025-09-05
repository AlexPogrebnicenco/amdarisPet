import React from "react";
import { Box, Stack, Typography, IconButton, useTheme } from "@mui/material";
import ArrowCircleRightTwoToneIcon from "@mui/icons-material/ArrowCircleRightTwoTone";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import { useNavigate } from "react-router-dom";
import RoleBasedRender from "../RoleBasedRender/RoleBasedRender";
import { deleteLesson } from "../../../services/lessonService";

interface LessonCardProps {
  courseId: number;
  title: string;
  orderNumber: number;
  onEdit?: () => void;
  onDelete?: (orderNumber: number) => void;
  selectedOrderNumber?: number | null;
  authorId: number;
}

const LessonCard: React.FC<LessonCardProps> = ({
  courseId,
  title,
  orderNumber,
  onEdit,
  onDelete,
  selectedOrderNumber,
  authorId
}) => {
  const navigate = useNavigate();
  const theme = useTheme();

  const handleDelete = async () => {
    if (!window.confirm("Are you sure you want to delete this lesson?")) return;

    try {
      await deleteLesson(courseId, orderNumber);
      onDelete && onDelete(orderNumber);
    } catch (error) {
      console.error("Error deleting lesson:", error);
    }
  };

  const handleCardClick = () => {
    if (selectedOrderNumber === orderNumber) {
      navigate(`/app/course/${courseId}`); 
    } else {
      navigate(`/app/course/${courseId}/lesson/${orderNumber}`); 
    }
  };
  
  return (
    <Box
      sx={{
        display: "flex",
        alignItems: "center",
        justifyContent: "space-between",
        mb: 1,
        p: 2,
        borderRadius: 2,
        cursor: "pointer",
        backgroundColor: theme.palette.card.background,
        transition: "background-color 0.3s ease",
        "&:hover": {
          backgroundColor: theme.palette.card.hoverBackground,
          "& .lesson-title": {
            color: theme.palette.text.secondary,
          },
        },
      }}
      onClick={handleCardClick}
    >
      <Stack direction="row" alignItems="center" spacing={1}>
        <ArrowCircleRightTwoToneIcon
          sx={{ color: theme.palette.button.main, fontSize: 40 }}
        />
        <Typography variant="body1" className="lesson-title">
          {`Lesson ${orderNumber}: ${title}`}
        </Typography>
      </Stack>

      <Stack direction="row" spacing={1} alignItems="center">
        <RoleBasedRender allowedRoles={["Teacher", "Admin"]} authorId={authorId}>
          {/* Edit button */}
          <IconButton
            onClick={(e) => {
              e.stopPropagation();
               navigate(`/app/course/${courseId}/lesson/${orderNumber}/edit`);
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
        </RoleBasedRender>

        <RoleBasedRender allowedRoles={["Teacher", "Admin"]} authorId={authorId} >
          {/* Delete button */}
          <IconButton
            onClick={(e) => {
              e.stopPropagation();
              handleDelete();
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
        </RoleBasedRender>
      </Stack>
    </Box>
  );
};

export default LessonCard;
