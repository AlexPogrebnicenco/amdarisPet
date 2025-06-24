import React, { useEffect, useMemo } from "react";
import { Box, Tabs, Tab, useTheme, Divider } from "@mui/material";
import { alpha } from "@mui/material/styles";
import useMediaQuery from "@mui/material/useMediaQuery";
import { useLocation, useNavigate } from "react-router-dom";

import CoursesFiltersPanel from "./ProfileFiltersPanel";
import CertificatesTab from "./CertificatesTab";
import SettingsTab from "./SettingsTab";

const tabLabels = [
  { label: "Certificates", route: ":certificates", component: <CertificatesTab />, count: 4 },
  { label: "Settings", route: ":settings", component: <SettingsTab /> },
];

const ProfileTabs: React.FC = () => {
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

  // 🔹 Определяем активный таб по route
  const currentTabIndex = tabLabels.findIndex((tab) =>
    location.pathname.includes(tab.route)
  );

  const tab = currentTabIndex === -1 ? 0 : currentTabIndex;

  const handleChange = (_: React.SyntheticEvent, newValue: number) => {
    navigate(`/profile${tabLabels[newValue].route}`);
  };

  return (
    <>
      <Box
        sx={{
          position: "sticky",
          top: topOffset,
          zIndex: 100,
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
          {tabLabels.map((tabItem, index) => (
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

        {tabLabels[tab].route !== ":settings" && <CoursesFiltersPanel />}
        <Divider />
      </Box>

      {/* 🔹 Выводим компонент выбранного таба */}
      <Box sx={{ mt: 2 }}>{tabLabels[tab]?.component}</Box>
    </>
  );
};

export default ProfileTabs;
