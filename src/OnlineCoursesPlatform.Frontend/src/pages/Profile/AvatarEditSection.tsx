import { useRef } from "react";
import { Avatar, Box, IconButton, Typography, useTheme } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";

interface AvatarEditSectionProps {
  avatarUrl: string;
  onChange: (file: File) => void;
}

const AvatarEditSection = ({ avatarUrl, onChange }: AvatarEditSectionProps) => {
  const theme = useTheme();

  const fileInputRef = useRef<HTMLInputElement | null>(null);

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    if (file) onChange(file);
  };

  return (
    <Box sx={{ my: 4 }}>
      <Typography variant="h6" sx={{ mb: 2, textAlign: "center" }}>
        Avatar
      </Typography>
      <Box sx={{ display: "flex", justifyContent: "center" }}>
        <Box sx={{ position: "relative" }}>
          <Avatar src={avatarUrl || undefined} sx={{ width: 150, height: 150 }}>
            {!avatarUrl && <AccountCircleIcon sx={{ fontSize: 170 }} />}
          </Avatar>
          <IconButton
            onClick={() => fileInputRef.current?.click()}
            sx={{
              position: "absolute",
              bottom: -10,
              right: -10,
              transition: "background-color 0.3s, color 0.3s",
              "&:hover": {
                backgroundColor: "transparent",
                "& svg": { color: "#FFD700" },
              },
            }}
          >
            <EditIcon sx={{ color: theme.palette.text.primary}} />
          </IconButton>
          <input
            type="file"
            accept="image/*"
            ref={fileInputRef}
            style={{ display: "none" }}
            onChange={handleFileChange}
          />
        </Box>
      </Box>
    </Box>
  );
};

export default AvatarEditSection;
