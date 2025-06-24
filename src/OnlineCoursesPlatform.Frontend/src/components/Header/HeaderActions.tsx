import { Box } from "@mui/material";
import MailButton from "../common/MailButton/MailButton";
import NotificationBell from "../common/NotificationBell/NotificationBell";
import ProfileMenuButton from "../common/ProfileMenuButton/ProfileMenuButton";
import MobileMenuButton from "../common/MobileMenuButton/MobileMenuButton";
import { useState } from "react";
import MobileMenu from "../common/MobileMenuButton/MobileMenu";

interface HeaderActionsProps {
  isMobileMenuOpen: boolean;
  mobileMenuId: string;
  menuId: string;
  onProfileMenuOpen: (event: React.MouseEvent<HTMLElement>) => void;
  onMobileMenuOpen: (event: React.MouseEvent<HTMLElement>) => void;
}

const HeaderActions = ({
  // isMobileMenuOpen,
  mobileMenuId,
  // onMobileMenuOpen,
}: HeaderActionsProps) => {
  const [mobileAnchorEl, setMobileAnchorEl] = useState<null | HTMLElement>(
    null
  );
  const isMobileOpen = Boolean(mobileAnchorEl);
  const handleMobileClick = (event: React.MouseEvent<HTMLElement>) => {
    setMobileAnchorEl(event.currentTarget);
  };
  const handleMobileClose = () => setMobileAnchorEl(null);
  return (
    <>
      <Box sx={{ display: { xs: "none", md: "flex" } }}>
        <MailButton />
        <NotificationBell />
        <ProfileMenuButton />
      </Box>

      <Box sx={{ display: { xs: "flex", md: "none" } }}>
        <MobileMenuButton
          mobileMenuId={mobileMenuId}
          onClick={handleMobileClick}
        />
      </Box>
      <MobileMenu
        anchorEl={mobileAnchorEl}
        open={isMobileOpen}
        onClose={handleMobileClose}
      >
        {[
          <MailButton />,
          <NotificationBell />,
          <ProfileMenuButton />,
        ]}
      </MobileMenu>
    </>
  );
};

export default HeaderActions;
