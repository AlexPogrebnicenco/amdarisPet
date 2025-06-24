import React, { useMemo, useState } from "react";
import { Box, Tabs, Tab, useTheme, } from "@mui/material";
import CoursesFiltersPanel from "./CourseFiltersPanel";
import { alpha } from "@mui/material/styles";
import useMediaQuery from "@mui/material/useMediaQuery";

const tabLabels = [
  { label: "Browse", count: 80 },
  { label: "Started", count: 3 },
  { label: "Completed", count: 1 },
];

const CoursesTabs: React.FC = () => {
  const [tab, setTab] = useState(0);

  const theme = useTheme();

  const isXS = useMediaQuery("(max-width:393px)");
  const isSM = useMediaQuery("(min-width:393px) and (max-width:599px)");

  const topOffset = useMemo(() => {
    if (isXS) return 55;
    if (isSM) return 47;
    return 63;
  }, [isXS, isSM]);

  const handleChange = (_: React.SyntheticEvent, newValue: number) => {
    setTab(newValue);
  };


  return (
    <Box
      sx={{
        position: "sticky",
        top: topOffset,
        zIndex: 100,
        backgroundColor: alpha(theme.palette.background.default, 0.8),
        backdropFilter: "blur(6px)", // для эффекта размытияs
      }}
    >
      <Tabs
        value={tab}
        onChange={handleChange}
        sx={{
          "& .MuiTabs-indicator": {
            backgroundColor: theme.palette.button.main,
          },
        }}
      >
        {tabLabels.map((tabItem, index) => (
          <Tab
            key={index}
            label={`${tabItem.label} (${tabItem.count})`}
            sx={{
              textTransform: "none",
              color: theme.palette.text.primary,
              "&:hover": {
                color: theme.palette.text.secondary,
              },
              "&.Mui-selected": {
                color: theme.palette.text.secondary,
              },
            }}
          />
        ))}
      </Tabs>

      <CoursesFiltersPanel />
      
    </Box>

  );
};

export default CoursesTabs;
