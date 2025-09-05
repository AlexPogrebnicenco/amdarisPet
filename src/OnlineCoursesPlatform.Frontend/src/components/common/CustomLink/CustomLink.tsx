import { Link, useTheme } from "@mui/material";
import { Link as RouterLink } from "react-router-dom";

interface CustomLinkProps {
  to: string;
  children: React.ReactNode;
}

const CustomLink = ({ to, children }: CustomLinkProps) => {
  const theme = useTheme();
  const isExternal = to.startsWith("http://") || to.startsWith("https://");

  return (
    <Link
      component={isExternal ? "a" : RouterLink}
      href={isExternal ? to : undefined}
      to={!isExternal ? to : undefined}
      target={isExternal ? "_blank" : undefined}
      rel={isExternal ? "noopener noreferrer" : undefined}
      sx={{
        display: "inline-block",
        textDecoration: "none",
        color: theme.palette.link.main,
        transition: "color 0.2s, transform 0.1s",
        "&:hover": {
          color: theme.palette.link.hover,
          textDecoration: "underline",
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
