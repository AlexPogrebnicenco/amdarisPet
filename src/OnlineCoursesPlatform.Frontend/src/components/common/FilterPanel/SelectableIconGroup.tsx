import React, { useState } from "react";
import { Box, IconButton } from "@mui/material";

interface IconItem {
  id: number;
  label: string;
  icon: React.ReactNode;
}

interface SelectableIconGroupProps {
  icons: IconItem[];
  onSelect: (selectedLabel: string) => void;
  activeColor?: string;
}

const SelectableIconGroup: React.FC<SelectableIconGroupProps> = ({
  icons,
  onSelect,
  activeColor = "#1c1c1c",
}) => {
  const [selected, setSelected] = useState<number | null>(null);

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
      {icons.map(({ id, icon, label }, index, arr) => {
        const isActive = selected === id;

        return (
          <Box
            key={id}
            onClick={() => {
              setSelected(id);
              onSelect(label);
            }}
            sx={{
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
              px: 1,
              cursor: "pointer",
              borderRight:
                index !== arr.length - 1 ? "1px solid #33363E" : "none",
              backgroundColor: isActive ? activeColor : "transparent",
              transition: "background-color 0.2s ease",
              "&:hover": {
                backgroundColor: activeColor,
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
                pointerEvents: "none", // кликабельность у Box
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

export default SelectableIconGroup;
