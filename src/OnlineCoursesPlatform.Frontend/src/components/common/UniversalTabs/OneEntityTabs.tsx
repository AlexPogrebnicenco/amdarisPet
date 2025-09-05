import React from "react";
import { Box, Tabs, Tab, useTheme, Divider } from "@mui/material";
import { alpha } from "@mui/material/styles";
import useMediaQuery from "@mui/material/useMediaQuery";
import { useSearchParams } from "react-router-dom";

export interface OneEntityTabItem {
  label: string;
  value: string;
}

interface OneEntityTabsProps {
  tabs: OneEntityTabItem[];
  filtersPanel?: (currentTab: OneEntityTabItem) => React.ReactNode;
  actionsPanel?: React.ReactNode;
}

const OneEntityTabs: React.FC<OneEntityTabsProps> = ({ tabs, filtersPanel, actionsPanel }) => {
  const theme = useTheme();
  const isXS = useMediaQuery("(max-width:393px)");
  const isSM = useMediaQuery("(min-width:393px) and (max-width:599px)");

  const topOffset = React.useMemo(() => {
    if (isXS) return 55;
    if (isSM) return 47;
    return 63;
  }, [isXS, isSM]);

  const [searchParams, setSearchParams] = useSearchParams();
  const currentTabValue = searchParams.get('tab') || tabs[0].value;

  const handleChange = (_: React.SyntheticEvent, newValue: string) => {
    setSearchParams({ tab: newValue });
  };

  return (
    <>
      <Box
        sx={{
          position: "sticky",
          top: topOffset,
          zIndex: 1500,
          backgroundColor: alpha(theme.palette.background.default, 0.8),
          backdropFilter: "blur(6px)",
        }}
      >  
        <Tabs
          value={currentTabValue}
          onChange={handleChange}
          sx={{
            "& .MuiTabs-indicator": {
              backgroundColor: theme.palette.button.main,
            },
          }}
        >
          {tabs.map((tabItem, index) => (
            <Tab
              key={index}
              label={tabItem.label}
              value={tabItem.value}
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
          <Divider />
        {filtersPanel && filtersPanel(tabs.find(tab => tab.value === currentTabValue)!)}
        {actionsPanel && <Box>{actionsPanel}</Box>}
        <Divider />
      </Box>
    </>
  );
};

export default OneEntityTabs;
