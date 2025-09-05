import { IconButton } from "@mui/material";
import MoreIcon from "@mui/icons-material/MoreVert";

interface MobileMenuButtonProps {
  mobileMenuId: string;
  onClick: (event: React.MouseEvent<HTMLElement>) => void;
}

const MobileMenuButton = ({ mobileMenuId, onClick }: MobileMenuButtonProps) => (
  <IconButton
    size="large"
    aria-label="show more"
    aria-controls={mobileMenuId}
    aria-haspopup="true"
    onClick={onClick}
    color="inherit"
  >
    <MoreIcon />
  </IconButton>
);

export default MobileMenuButton;
