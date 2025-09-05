import CodeIcon from '@mui/icons-material/Code';
import SecurityIcon from '@mui/icons-material/Security';
import DataObjectIcon from '@mui/icons-material/DataObject';
import SettingsInputComponentIcon from '@mui/icons-material/SettingsInputComponent';
import WebIcon from '@mui/icons-material/Web';
import SportsEsportsIcon from '@mui/icons-material/SportsEsports';
import SmartphoneIcon from '@mui/icons-material/Smartphone';
import BusinessCenterIcon from '@mui/icons-material/BusinessCenter';
import BugReportIcon from '@mui/icons-material/BugReport';
import DesignServicesIcon from '@mui/icons-material/DesignServices';

export const courseCategories = [
  {
    name: "Backend",
    icon: <CodeIcon sx={{ fontSize: 12, color: "#4caf50" }} />,
  },
  {
    name: "Cybersecurity",
    icon: <SecurityIcon sx={{ fontSize: 12, color: "#ff5722" }} />,
  },
  {
    name: "Data Science",
    icon: <DataObjectIcon sx={{ fontSize: 12, color: "#00bcd4" }} />,
  },
  {
    name: "DevOps",
    icon: <SettingsInputComponentIcon sx={{ fontSize: 12, color: "#8bc34a" }} />,
  },
  {
    name: "Frontend",
    icon: <WebIcon sx={{ fontSize: 12, color: "#2196f3" }} />,
  },
  {
    name: "Game Development",
    icon: <SportsEsportsIcon sx={{ fontSize: 12, color: "#9c27b0" }} />,
  },
  {
    name: "Mobile Development",
    icon: <SmartphoneIcon sx={{ fontSize: 12, color: "#f44336" }} />,
  },
  {
    name: "Project Management",
    icon: <BusinessCenterIcon sx={{ fontSize: 12, color: "#ff9800" }} />,
  },
  {
    name: "QA / Testing",
    icon: <BugReportIcon sx={{ fontSize: 12, color: "#607d8b" }} />,
  },
  {
    name: "UI/UX Design",
    icon: <DesignServicesIcon sx={{ fontSize: 12, color: "#e91e63" }} />,
  },
];
