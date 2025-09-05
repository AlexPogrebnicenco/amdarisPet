import React, { useMemo } from "react";
import { Box, Tabs, Tab, useTheme, Divider } from "@mui/material";
import { alpha } from "@mui/material/styles";
import useMediaQuery from "@mui/material/useMediaQuery";
import { useLocation, useNavigate } from "react-router-dom";

export interface TabItem {
  label: string;
  route: string;
  count?: number;
}

interface UniversalTabsProps {
  tabs: TabItem[];
  filtersPanel?: (currentTab: TabItem) => React.ReactNode;
}

const UniversalTabs: React.FC<UniversalTabsProps> = ({
  tabs,
  filtersPanel,
}) => {
  const theme = useTheme();
  const isXS = useMediaQuery("(max-width:393px)");
  const isSM = useMediaQuery("(min-width:393px) and (max-width:599px)");

  const topOffset = useMemo(() => {
    if (isXS) return 55;
    if (isSM) return 47;
    return 63;
  }, [isXS, isSM]);

  const location = useLocation();
  const navigate = useNavigate();

  const currentTabIndex = tabs.findIndex((tab) =>
    location.pathname.includes(tab.route)
  );

  const tab = currentTabIndex === -1 ? 0 : currentTabIndex;

  const handleChange = (_: React.SyntheticEvent, newValue: number) => {
    navigate(tabs[newValue].route);
  };

  return (
    <>
      <Box
        sx={{
          position: "sticky",
          top: topOffset,
          zIndex: 1200,
          backgroundColor: alpha(theme.palette.background.default, 0.8),
          backdropFilter: "blur(6px)",
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
          {tabs.map((tabItem, index) => (
            <Tab
              key={index}
              label={
                tabItem.count !== undefined
                  ? `${tabItem.label} (${tabItem.count})`
                  : tabItem.label
              }
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

        {filtersPanel && filtersPanel(tabs[tab])}
        <Divider />
      </Box>
    </>
  );
};

export default UniversalTabs;
