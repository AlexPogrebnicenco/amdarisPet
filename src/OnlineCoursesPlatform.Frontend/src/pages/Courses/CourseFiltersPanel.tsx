import React, { useState } from "react";
import { Box, Divider, useMediaQuery, useTheme } from "@mui/material";
import IconFilterGroup from "../../components/common/IconFilterGroup/IconFilterGroup";
import ViewMenuButton from "../../components/common/ViewMenuButton/ViewMenuButton";
import SearchFilter from "../../components/common/SearchFiler/SearchFilter";
import SortOrderMenu from "../../components/common/SortOrderMenu/SortOrderMenu";

interface CoursesFiltersPanelProps {
  onSortChange: (sort: string) => void;
  onTagChange: (tag: string | null) => void;
  onSearch: (query: string) => void;
}

const CoursesFiltersPanel: React.FC<CoursesFiltersPanelProps> = ({
  onSortChange,
  onTagChange,
  onSearch,
}) => {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down("md"));
  const [searchTerm, setSearchTerm] = useState("");
  const [selectedTagId, setSelectedTagId] = useState<number | null>(null);
  const [selectedSort, setSelectedSort] = useState("lastCreated");

  return (
    <>
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
            maxWidth: "100%",
            overflowX: isMobile ? "auto" : "visible",
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
              flexShrink: 0,
            }}
          >
            <ViewMenuButton
              onClearFilters={() => {
                onTagChange(null);
                setSelectedTagId(null);
                setSearchTerm("");
                onSearch("");
              }}
              onRestoreDefaults={() => {
                onTagChange(null);
                setSelectedTagId(null);
                setSearchTerm("");
                onSearch("");
                onSortChange("lastCreated");
                setSelectedSort("lastCreated");
              }}
            />
            <IconFilterGroup
              selected={selectedTagId}
              setSelected={setSelectedTagId}
              onTagSelect={onTagChange}
            />
          </Box>

          <SearchFilter
            searchTerm={searchTerm}
            setSearchTerm={setSearchTerm}
            onSearch={onSearch}
          />

          <SortOrderMenu
            onChange={(value) => {
              setSelectedSort(value);
              onSortChange(value);
            }}
            defaultValue={selectedSort}
          />
        </Box>
      </Box>

      <Divider />
    </>
  );
};

export default CoursesFiltersPanel;
