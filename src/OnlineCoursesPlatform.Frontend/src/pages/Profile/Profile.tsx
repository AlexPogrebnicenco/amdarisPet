import { Avatar, Box, Typography, useTheme } from "@mui/material";
import CommonButton from "../../components/common/CommonButton/CommonButton";
import ProfileTabs from "./ProfileTabs";
import { useNavigate } from "react-router-dom";

const Profile = () => {
  const theme = useTheme();
  const navigate = useNavigate();
  const handleSignOut = () => {
    // Здесь можно также очистить токен или состояние авторизации
    navigate("/login");
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


