import type { LessonList } from "../../../services/lessonService";
import AddLessonForm from "./AddLessonForm";
import { Box, Paper, useTheme } from "@mui/material";

interface AddLessonTabProps {
  onLessonCreated?: () => void;
  existingLessons: LessonList[];
  courseId: number;
  courseTitle: string;
}

const AddLessonTab: React.FC<AddLessonTabProps> = ({
  onLessonCreated,
  existingLessons,
  courseId,
  courseTitle,
}) => {
  const theme = useTheme();

  return (
    <Box
      sx={{
        backgroundColor: theme.palette.background.default,
        display: "flex",
        justifyContent: "center",
        alignItems: "flex-start",
        px: 1,
        mt: 2,
        boxSizing: "border-box",
      }}
    >
      <Paper
        elevation={6}
        sx={{
          p: { xs: 3, sm: 4 },
          width: "90%",
          backgroundColor: theme.palette.background.paper,
          borderRadius: 3,
          boxShadow: "0 8px 24px rgba(0, 0, 0, 0.3)",
        }}
      >
        <AddLessonForm
          onLessonCreated={onLessonCreated}
          existingLessons={existingLessons}
          courseId={courseId}
          courseTitle={courseTitle}
        />
      </Paper>
    </Box>
  );
};

export default AddLessonTab;
