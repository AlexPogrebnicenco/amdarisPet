// src/components/CustomLink.tsx
import { Link, useTheme } from "@mui/material";
import { Link as RouterLink } from "react-router-dom";

interface CustomLinkProps {
  to: string;
  children: React.ReactNode;
}

const CustomLink = ({ to, children }: CustomLinkProps) => {
  const theme = useTheme();

  return (
    <Link
      component={RouterLink}
      to={to}
      sx={{
        display: "inline-block",
        textDecoration: "none",
        color: theme.palette.link.main,
        transition: "color 0.2s, transform 0.1s",
        "&:hover": {
          color: theme.palette.link.hover,
        },
        "&:active": {
          transform: "scale(1.05)",
        },
      }}
    >
      {children}
    </Link>
  );
};

export default CustomLink;
