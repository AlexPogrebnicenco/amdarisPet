import React from "react";
import { Box, Divider, useMediaQuery, useTheme } from "@mui/material";
import IconFilterGroup from "./IconFilterGroup";
import ViewMenuButton from "../../components/common/ViewMenuButton/ViewMenuButton";
import SearchFilter from "../../components/common/SearchFiler/SearchFilter";
import SortOrderMenu from "../../components/common/SortOrderMenu/SortOrderMenu";

const CoursesFiltersPanel: React.FC = () => {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down("md"));
  return (
    <>
      {/* Divider */}
      <Divider />

      <Box sx={{ overflowX: isMobile ? "auto" : "visible", width: "100%" }}>
        <Box
          sx={{
            display: "flex",
            flexWrap: isMobile ? "nowrap" : "wrap",
            justifyContent: "space-between",
            alignItems: "center",
            gap: 2,
            mt: 1,
            mb: 1,
            maxWidth: "100%", // 🔹 важно!
            overflowX: isMobile ? "auto" : "visible", // 🔹 именно тут скролл
          }}
        >
          {/* Left side: filters */}
          <Box
            sx={{
              display: "flex",
              flexWrap: "nowrap",
              alignItems: "center",
              gap: 1,
              height: 32,
              flexShrink: 0, // 🔹 не даёт растягивать родителя
            }}
          >
            <ViewMenuButton />
            <IconFilterGroup />
          </Box>

          
            <SearchFilter />
          

        
            <SortOrderMenu />
          
        </Box>
      </Box>

      <Divider />
    </>
  );
};

export default CoursesFiltersPanel;
