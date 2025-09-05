import { Avatar, Box, Typography, useTheme } from "@mui/material";
import CommonButton from "../../components/common/CommonButton/CommonButton";
import ProfileTabs from "./ProfileTabs";
import { useNavigate } from "react-router-dom";
import axiosInstance from "../../api/axios";
import { useAuth } from "../../context/AuthContext";
import { useCourse } from "../../context/CourseContext";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import EditAccountModal from "./EditAccountModal";
import { useState } from "react";

const Profile = () => {
  const theme = useTheme();
  const navigate = useNavigate();
  const { logout, avatarUrl, userName } = useAuth();
  const { resetCourseId } = useCourse();
  const [editOpen, setEditOpen] = useState(false);

  const handleSignOut = async () => {
    try {
      await axiosInstance.get("/auth/logout"); // запрос на бэк для очистки cookies
      logout(); //  вызываем глобальный logout
      resetCourseId(); // сбрасываем выбранный курс
      navigate("/login"); // перенаправляем
    } catch (error) {
      console.error("Logout failed:", error);
    }
  };

  return (
    <Box>
      {/* Profile top section */}
      <Box
        sx={{
          display: "flex",
          alignItems: "center",
          gap: 2,
          mb: 2,
          flexWrap: "wrap",
        }}
      >
        <Avatar
          src={avatarUrl || undefined}
          sx={{
            width: 100,
            height: 100,
            bgcolor: avatarUrl ? "transparent" : theme.palette.grey[400],
          }}
        >
          {!avatarUrl && <AccountCircleIcon sx={{ fontSize: 110 }} />}
        </Avatar>
        <Box sx={{ flexGrow: 1 }}>
          <Typography
            variant="h4"
            gutterBottom
            sx={{ color: theme.palette.text.secondary }}
          >
            {userName}
          </Typography>

          <Box sx={{ display: "flex", gap: 1, mt: 1 }}>
            <CommonButton onClick={handleSignOut}>SIGN OUT</CommonButton>
            <CommonButton onClick={() => setEditOpen(true)}>EDIT PROFILE</CommonButton>
            <EditAccountModal open={editOpen} onClose={() => setEditOpen(false)} />
          </Box>
        </Box>
      </Box>

      <ProfileTabs />
    </Box>
  );
};

export default Profile;
