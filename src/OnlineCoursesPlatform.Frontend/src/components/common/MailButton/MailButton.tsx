import React, { useState } from "react";
import { IconButton, Badge, Tooltip } from "@mui/material";
import MailIcon from "@mui/icons-material/Mail";
import BasicMenu from "../BasicMenu/BasicMenu";
import { useTheme } from "@mui/material/styles";

const messages = [
  { id: 0, label: "First mesage" },
  { id: 1, label: "Second message" },
  { id: 2, label: "Third message" },
];

const MailButton = () => {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

  const handleOpen = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const open = Boolean(anchorEl);
  const tooltipText = messages.length
    ? `You have ${messages.length} new messages!`
    : "No new messages";
  const theme = useTheme();

  return (
    <>
      <Tooltip title={tooltipText}>
        <IconButton
          size="large"
          aria-label="show mails"
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
            badgeContent={messages.length}
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
            <MailIcon />
          </Badge>
        </IconButton>
      </Tooltip>

      <BasicMenu
        disableScrollLock
        anchorEl={anchorEl}
        open={open}
        handleClose={handleClose}
        menuItems={messages}
        sx={{ zIndex: 1600 }}
        onSelect={() => console.log("Hi")}
      />
    </>
  );
};

export default MailButton;
