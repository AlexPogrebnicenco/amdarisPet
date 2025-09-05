import ChangeHistoryOutlinedIcon from "@mui/icons-material/ChangeHistoryOutlined";
import TrendingUpIcon from "@mui/icons-material/TrendingUp";
import WhatshotIcon from "@mui/icons-material/Whatshot";

export const difficultyLevels = [
  {
    level: "Beginner",
    icon: <ChangeHistoryOutlinedIcon sx={{ fontSize: 12, color: "#81C784" }} />, 
  },
  {
    level: "Intermediate",
    icon: <TrendingUpIcon sx={{ fontSize: 12, color: "#FFB74D" }} />, 
  },
  {
    level: "Advanced",
    icon: <WhatshotIcon sx={{ fontSize: 12, color: "#E57373" }} />, 
  },
];
