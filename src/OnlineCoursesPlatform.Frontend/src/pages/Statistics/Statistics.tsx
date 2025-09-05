import { Box, Typography } from "@mui/material";
import ConstructionTwoToneIcon from "@mui/icons-material/ConstructionTwoTone";
const Statistics = () => {
  return (
    <Box
      sx={{
        height: "60vh",
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
        textAlign: "center",
        gap: 2,
      }}
    >
      <ConstructionTwoToneIcon
        sx={{
          fontSize: 64,
          color: "#FFA000",
          animation: "pulse 1.5s infinite ease-in-out",
          "@keyframes pulse": {
            "0%": { transform: "scale(1)" },
            "50%": { transform: "scale(1.1)" },
            "100%": { transform: "scale(1)" },
          },
        }}
      />
      <Typography variant="h5" sx={{ fontWeight: "bold" }}>
        This page is under construction
      </Typography>
      <Typography variant="body1" color="text.primary">
        We're working hard to bring you this feature soon!
      </Typography>
    </Box>
  );
};

export default Statistics;
