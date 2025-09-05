import React, { useEffect, useState } from "react";
import Badge from "@mui/material/Badge";
import NotificationIcon from "@mui/icons-material/Notifications";
import IconButton from "@mui/material/IconButton";
import Tooltip from "@mui/material/Tooltip";
import BasicMenu from "../BasicMenu/BasicMenu";
import { useTheme } from "@mui/material/styles";
import {
  startNotificationConnection,
  stopNotificationConnection,
} from "../../../services/notificationService";
import { useAuth } from "../../../context/AuthContext";

const NotificationBell = () => {
  const [notifications, setNotifications] = useState<
    { id: number; label: string }[]
  >([]);
  const [anchorEl, setAnchorEl] = useState<HTMLElement | null>(null);

  const { userId, role, isAuthenticated } = useAuth();

  useEffect(() => {
    if (isAuthenticated && role === "Teacher" && userId) {
      startNotificationConnection(userId, (notification) => {
        setNotifications((prev) => [
          ...prev,
          {
            id: prev.length,
            label: `New enrollment: ${notification.userEmail} on ${notification.courseTitle}`,
          },
        ]);
      });
    }

    return () => {
      stopNotificationConnection(); // Отключаем при размонтировании
    };
  }, [isAuthenticated, role, userId]);

  const handleOpen = (event: React.MouseEvent<HTMLButtonElement>) => {
    if (notifications.length > 0) {
      setAnchorEl(event.currentTarget);
    }
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleSelectNotification = (item: { id: number; label: string }) => {
    console.log("Selected notification:", item);
    // Убираем уведомление после клика
    setNotifications((prev) => prev.filter((n) => n.id !== item.id));
  };

  const open = Boolean(anchorEl);
  const tooltipText = notifications.length
    ? `You have ${notifications.length} new notifications!`
    : "No new notifications";
  const theme = useTheme();

  return (
    <>
      <Tooltip title={tooltipText}>
        <IconButton
          size="large"
          aria-label="show notifications"
          onClick={handleOpen}
          sx={{
            color: open
              ? theme.palette.text.secondary
              : theme.palette.text.primary,
            transform: open ? "scale(1.1)" : "scale(1)",
            transition: "all 0.2s ease-in-out",
            "&:hover": {
              color: theme.palette.text.secondary,
              transform: "scale(1.1)",
            },
          }}
        >
          <Badge
            badgeContent={notifications.length}
            sx={{
              "& .MuiBadge-badge": {
                backgroundColor: theme.palette.error.main,
                color: open
                  ? theme.palette.text.secondary
                  : theme.palette.text.primary,
                transition: "color 0.2s ease-in-out",
              },
              "&:hover .MuiBadge-badge": {
                color: theme.palette.text.secondary,
              },
            }}
          >
            <NotificationIcon />
          </Badge>
        </IconButton>
      </Tooltip>
      <BasicMenu
        disableScrollLock
        anchorEl={anchorEl}
        open={open}
        handleClose={handleClose}
        menuItems={notifications}
        onSelect={handleSelectNotification}
        sx={{ zIndex: 2000 }}
      />
    </>
  );
};

export default NotificationBell;
