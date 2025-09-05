import { Box, Button, Container, Typography, useTheme } from "@mui/material";
import { motion } from "framer-motion";
import { useNavigate } from "react-router-dom";
import { FaChalkboardTeacher, FaGraduationCap, FaVideo } from "react-icons/fa";
import { PiStudentFill } from "react-icons/pi";
import ArrowForwardIcon from "@mui/icons-material/ArrowForward";

const featureItems = [
  {
    icon: <FaGraduationCap size={32} />,
    title: "Free Quality Content",
    description:
      "Access tons of high-quality video and markdown lessons created by passionate educators.",
  },
  {
    icon: <FaChalkboardTeacher size={32} />,
    title: "No Middlemen",
    description:
      "Teachers earn directly from subscriptions without giving a cut to anyone else.",
  },
  {
    icon: <FaVideo size={32} />,
    title: "Video or Markdown Lessons",
    description:
      "Choose how you learn – via engaging videos or structured text lessons.",
  },
  {
    icon: <PiStudentFill size={32} />,
    title: "For Everyone Over 13",
    description:
      "Beginner, intermediate, or advanced — there's something here for everyone.",
  },
];

const Home = () => {
  const theme = useTheme();
  const navigate = useNavigate();

  return (
    <Box sx={{ color: theme.palette.text.primary }}>
      <Box
        sx={{
          position: "relative",
          py: 10,
          textAlign: "center",
          overflow: "hidden",
          background: `radial-gradient(circle at top left, #1f2b3a 0%, #0f2027 40%, #0a161f 100%)`,
          "::before": {
            content: '""',
            position: "absolute",
            top: "-20%",
            left: "-20%",
            width: "140%",
            height: "140%",
            background: `radial-gradient(circle, rgba(255,255,255,0.05) 0%, transparent 60%)`,
            animation: "rotateGlow 20s linear infinite",
            zIndex: 0,
          },
          "::after": {
            content: '""',
            position: "absolute",
            bottom: 0,
            left: 0,
            width: "100%",
            height: "80px",
            background: "linear-gradient(to bottom, transparent, #161A1D)",
            zIndex: 1,
          },
          "::before, ::after, .topFade, .leftFade, .rightFade": {
            pointerEvents: "none",
          },
          "&::before": {
            animation: "rotateGlow 20s linear infinite",
          },
          "& .topFade": {
            content: '""',
            position: "absolute",
            top: 0,
            left: 0,
            width: "100%",
            height: "80px",
            background: "linear-gradient(to top, transparent, #161A1D)",
            zIndex: 1,
          },
          "& .leftFade": {
            content: '""',
            position: "absolute",
            top: 0,
            left: 0,
            width: "80px",
            height: "100%",
            background: "linear-gradient(to left, transparent, #161A1D)",
            zIndex: 1,
          },
          "& .rightFade": {
            content: '""',
            position: "absolute",
            top: 0,
            right: 0,
            width: "80px",
            height: "100%",
            background: "linear-gradient(to right, transparent, #161A1D)",
            zIndex: 1,
          },
          "@keyframes rotateGlow": {
            "0%": { transform: "rotate(0deg)" },
            "100%": { transform: "rotate(360deg)" },
          },
          zIndex: 1,
        }}
      >
        {" "}
        <Box className="topFade" />
        <Box className="leftFade" />
        <Box className="rightFade" />
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.8 }}
        >
          <Typography
            variant="h2"
            fontFamily="Audiowide"
            gutterBottom
            sx={{ fontWeight: "bold" }}
          >
            /OCP\
          </Typography>

          <Typography variant="h5" mb={4}>
            Code your future with OCP
          </Typography>

          <Button
            variant="contained"
            size="large"
            onClick={() => navigate("/app/courses")}
            endIcon={<ArrowForwardIcon />}
            sx={{
              backgroundColor: theme.palette.button.main,
              color: "#fff",
              fontWeight: "bold",
              fontSize: "1rem",
              px: 4,
              py: 1.5,
              borderRadius: "50px",
              animation: "bounce 1.5s ease-in-out infinite",
              boxShadow: "0 4px 10px rgba(3, 105, 161, 0.3)",
              transition: "all 0.3s ease",
              "&:hover": {
                backgroundColor: theme.palette.button.hover,
                boxShadow: "0 6px 14px rgba(12, 133, 198, 0.4)",
                transform: "translateY(-2px)",
              },
              "@keyframes bounce": {
                "0%, 100%": { transform: "translateY(0)" },
                "50%": { transform: "translateY(-6px)" },
              },
            }}
          >
            Start Learning
          </Button>
        </motion.div>
      </Box>

      <Container sx={{ py: 8 }}>
        <Typography variant="h4" textAlign="center" gutterBottom>
          Why Choose OCP?
        </Typography>

        <Box
          sx={{
            display: "grid",
            gridTemplateColumns: {
              xs: "1fr",
              sm: "1fr 1fr",
              md: "1fr 1fr",
              lg: "1fr 1fr",
            },
            gap: 4,
            mt: 4,
          }}
        >
          {featureItems.map((item, index) => (
            <motion.div
              key={index}
              whileHover={{ scale: 1.05 }}
              transition={{ duration: 0.3 }}
            >
              <Box
                sx={{
                  backgroundColor: theme.palette.background.paper,
                  borderRadius: 2,
                  p: 3,
                  boxShadow: 3,
                  height: "100%",
                }}
              >
                <Box sx={{ display: "flex", alignItems: "center", gap: 2 }}>
                  {item.icon}
                  <Typography variant="h6">{item.title}</Typography>
                </Box>
                <Typography variant="body2" mt={1}>
                  {item.description}
                </Typography>
              </Box>
            </motion.div>
          ))}
        </Box>

        <Typography
          variant="subtitle1"
          color="text.primary"
          textAlign="center"
          mt={8}
        >
          OCP is currently in development. Everything is free — join now and
          start learning!
        </Typography>
      </Container>
    </Box>
  );
};

export default Home;
