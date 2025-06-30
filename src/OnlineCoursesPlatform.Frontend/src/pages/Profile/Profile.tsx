import { Avatar, Box, Typography, useTheme } from "@mui/material";
import CommonButton from "../../components/common/CommonButton/CommonButton";
import ProfileTabs from "./ProfileTabs";
import { useNavigate } from "react-router-dom";
import axiosInstance from "../../api/axios";
import { useAuth } from "../../context/AuthContext"; 

const Profile = () => {
  const theme = useTheme();
  const navigate = useNavigate();
  const { logout } = useAuth(); //  подключил logout из контекста

  const handleSignOut = async () => {
    try {
      await axiosInstance.get('/auth/logout'); // запрос на бэк для очистки cookies
      logout(); //  вызываем глобальный logout
      navigate("/login"); // перенаправляем
    } catch (error) {
      console.error('Logout failed:', error);
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
          src="https://mui.com/static/images/avatar/1.jpg"
          sx={{ width: 100, height: 100 }}
        />
        <Box sx={{ flexGrow: 1 }}>
          <Typography
            variant="h4"
            gutterBottom
            sx={{ color: theme.palette.text.secondary }}
          >
            Alexandru Pogrebnicenco
          </Typography>

          <Box sx={{ display: "flex", gap: 1, mt: 1 }}>
            <CommonButton onClick={handleSignOut}>SIGN OUT</CommonButton>
            <CommonButton>EDIT PROFILE</CommonButton>
          </Box>
        </Box>
      </Box>

      <ProfileTabs />
    </Box>
  );
};

export default Profile;
