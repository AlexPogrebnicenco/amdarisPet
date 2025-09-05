import { motion } from "framer-motion";
import AddCourseForm from "./AddCourseForm";
import { Box, Paper, useMediaQuery, useTheme } from "@mui/material";
import { useNavigate } from "react-router-dom";

const AddCourseTab = () => {
  const theme = useTheme();
  const navigate = useNavigate();
  const isMobile = useMediaQuery(theme.breakpoints.down("sm"));

  return (
     <motion.div
      initial={{ opacity: 0, y: 10 }}
      animate={{ opacity: 1, y: 0 }}
      exit={{ opacity: 0, y: 10 }}
      transition={{ duration: 0.3, ease: "easeInOut" }}
    >
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
          <AddCourseForm onCourseCreated={() => navigate("/app/teacher/my-courses")} />
        </Paper>
      </Box>
    </motion.div>
  );
};

export default AddCourseTab;
