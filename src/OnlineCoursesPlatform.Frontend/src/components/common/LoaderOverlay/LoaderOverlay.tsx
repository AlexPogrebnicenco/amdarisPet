import React from "react";
import { Box, CircularProgress } from "@mui/material";

interface LoaderOverlayProps {
  isLoading: boolean;
}

const LoaderOverlay: React.FC<LoaderOverlayProps> = ({ isLoading }) => {
  if (!isLoading) return null;

  return (
    <Box
      sx={{
        position: "fixed",
        top: 0,
        left: 0,
        width: "100%",
        height: "100%",
        backgroundColor: "rgba(0, 0, 0, 0.6)",
        zIndex: 9999,
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
      }}
    >
      <CircularProgress sx={{ color: "#fff" }} />
    </Box>
  );
};

export default LoaderOverlay;
