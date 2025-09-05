import { Box, Typography, useTheme } from "@mui/material";
import Markdown from "@uiw/react-markdown-preview";

interface CourseAboutProps {
  about?: string | null;
}

const CourseAbout: React.FC<CourseAboutProps> = ({ about }) => {
  const theme = useTheme();
  return (
    <Box sx={{mt:2}}>
      {about ? (
        <Box data-color-mode="dark" style={{ width: "100%" }}>
          <Markdown
            source={about}
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
    </Box>
  );
};

export default CourseAbout;
