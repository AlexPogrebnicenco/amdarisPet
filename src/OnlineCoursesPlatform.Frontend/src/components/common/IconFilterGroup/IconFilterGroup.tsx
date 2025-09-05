import React from "react";
import { Box, IconButton } from "@mui/material";
import {
  SiReact,
  SiJavascript,
  SiDotnet,
  SiTypescript,
  SiTailwindcss,
} from "react-icons/si";

const techIcons = [
  {
    id: 0,
    label: "React",
    icon: <SiReact size={18} style={{ color: "#00D8FF" }} />,
  },
  {
    id: 1,
    label: "TypeScript",
    icon: <SiTypescript size={18} style={{ color: "#007BCD" }} />,
  },
  {
    id: 2,
    label: "JavaScript",
    icon: <SiJavascript size={18} style={{ color: "#FFD600" }} />,
  },
  {
    id: 3,
    label: "Tailwind",
    icon: <SiTailwindcss size={18} style={{ color: "#00BCFF" }} />,
  },
  {
    id: 4,
    label: ".NET",
    icon: <SiDotnet size={18} style={{ color: "#9B6AEF" }} />,
  },
];

interface IconFilterGroupProps {
  selected: number | null;
  setSelected: (id: number | null) => void;
  onTagSelect: (tag: string | null) => void;
}

const IconFilterGroup: React.FC<IconFilterGroupProps> = ({ onTagSelect,setSelected,selected }) => {

  return (
    <Box
      sx={{
        display: "flex",
        border: "1px solid #33363E",
        borderRadius: 1,
        overflow: "hidden",
        height: "100%",
      }}
    >
      {techIcons.map(({ id, icon, label }, index, arr) => {
        const isActive = selected === id;
        return (
          <Box
            key={id}
            onClick={() => {
              const newSelected = selected === id ? null : id;
              setSelected(newSelected);
              const tag = newSelected === null ? null : label;
              onTagSelect(tag);
            }}
            sx={{
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
              px: 1,
              cursor: "pointer",
              borderRight:
                index !== arr.length - 1 ? "1px solid #33363E" : "none",
              backgroundColor: isActive ? "#1c1c1c" : "transparent",
              transition: "background-color 0.2s ease",
              "&:hover": {
                backgroundColor: "#1c1c1c",
              },
              "&:hover .icon": {
                opacity: 1,
              },
            }}
          >
            <IconButton
              size="small"
              disableRipple
              sx={{
                p: 0.5,
                pointerEvents: "none",
              }}
            >
              <Box
                className="icon"
                sx={{
                  display: "flex",
                  alignItems: "center",
                  justifyContent: "center",
                  height: "100%",
                  opacity: isActive ? 1 : 0.6,
                  transition: "opacity 0.2s ease",
                }}
              >
                {icon}
              </Box>
            </IconButton>
          </Box>
        );
      })}
    </Box>
  );
};

export default IconFilterGroup;
