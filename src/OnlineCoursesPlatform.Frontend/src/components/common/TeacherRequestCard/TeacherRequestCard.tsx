import { Box, Typography, Button, useTheme } from "@mui/material";

interface TeacherRequestCardProps {
  userName: string;
  email: string;
  requestDate: string;
  onApprove: () => void;
  onReject: () => void;
}

const TeacherRequestCard: React.FC<TeacherRequestCardProps> = ({
  userName,
  email,
  requestDate,
  onApprove,
  onReject,
}) => {
  const theme = useTheme();

  return (
    <Box
      sx={{
        backgroundColor: theme.palette.card.background,
        borderRadius: 2,
        p: 3,
        display: "flex",
        flexDirection: "column",
        justifyContent: "space-between",
        height: "100%",
        minHeight: 250,
        maxHeight: 250,
        overflow: "hidden",
      }}
    >
      {/* Имя и почта */}
      <Box sx={{ mb: 2 }}>
        <Typography variant="h6" sx={{ color: theme.palette.card.text }}>
          {userName}
        </Typography>
        <Typography
          variant="body2"
          sx={{ color: theme.palette.card.mutedText }}
        >
          {email}
        </Typography>
      </Box>

      {/* Дата заявки */}
      <Typography
        variant="body2"
        sx={{ mb: 2, color: theme.palette.card.mutedText }}
      >
        Requested on:{" "}
        {new Date(requestDate).toLocaleString("en-GB", {
          year: "numeric",
          month: "long",
          day: "numeric",
          hour: "2-digit",
          minute: "2-digit",
          hour12: false,
        })}
      </Typography>

      {/* Кнопки */}
      <Box sx={{ display: "flex", gap: 1 }}>
        <Button fullWidth variant="contained" color="success" onClick={onApprove}>
          Approve
        </Button>
        <Button fullWidth variant="contained" color="error" onClick={onReject}>
          Reject
        </Button>
      </Box>
    </Box>
  );
};

export default TeacherRequestCard;
