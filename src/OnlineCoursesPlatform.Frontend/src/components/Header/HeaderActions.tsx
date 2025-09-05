import { Box, ClickAwayListener } from "@mui/material";
import MailButton from "../common/MailButton/MailButton";
import NotificationBell from "../common/NotificationBell/NotificationBell";
import ProfileMenuButton from "../common/ProfileMenuButton/ProfileMenuButton";
import MobileMenuButton from "../common/MobileMenuButton/MobileMenuButton";

interface HeaderActionsProps {
  mobileMenuId: string;
  isMobileMenuOpen: boolean;
  onMobileMenuOpen: () => void;
  onMobileMenuClose: () => void;
}

const HeaderActions = ({
  mobileMenuId,
  isMobileMenuOpen,
  onMobileMenuOpen,
  onMobileMenuClose,
}: HeaderActionsProps) => {
  return (
    <>
      {/* Desktop */}
      <Box sx={{ display: { xs: "none", md: "flex" } }}>
        <MailButton />
        <NotificationBell />
        <ProfileMenuButton />
      </Box>

      {/* Mobile menu toggle */}
      <Box sx={{ display: { xs: "flex", md: "none" } }}>
        <MobileMenuButton
          mobileMenuId={mobileMenuId}
          onClick={onMobileMenuOpen}
        />
      </Box>

      {/* Mobile menu */}
      {/* Custom mobile menu */}
      {isMobileMenuOpen && (
        <ClickAwayListener onClickAway={onMobileMenuClose}>
          <Box
            sx={{
              position: "fixed",
              top: 56,
              right: 16,
              zIndex: 1300,
              bgcolor: "background.paper",
              boxShadow: 3,
              borderRadius: 1,
              p: 1,
              display: "flex",
              flexDirection: "column",
            }}
          >
            <MailButton />
            <NotificationBell />
            <Box sx={{ pl: 1 }}>
              <ProfileMenuButton />
            </Box>
          </Box>
        </ClickAwayListener>
      )}
    </>
  );
};

export default HeaderActions;
