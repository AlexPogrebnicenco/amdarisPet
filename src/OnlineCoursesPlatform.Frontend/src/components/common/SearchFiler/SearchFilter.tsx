import { styled, alpha } from "@mui/material/styles";
import InputBase from "@mui/material/InputBase";
import Box from "@mui/material/Box";
import { useEffect } from "react";
import { MdFilterList } from "react-icons/md";
import debounce from "lodash.debounce";
import CloseIcon from "@mui/icons-material/Close";

interface SearchFilterProps {
  searchTerm: string;
  setSearchTerm: (value: string) => void;
  onSearch: (query: string) => void;
}

const SearchContainer = styled("div")(({ theme }) => ({
  position: "relative",
  height: "100%",
  borderRadius: theme.shape.borderRadius,
  backgroundColor: alpha(theme.palette.common.white, 0.1),
  "&:hover": {
    backgroundColor: alpha(theme.palette.common.white, 0.15),
  },
  marginRight: theme.spacing(2),
  marginLeft: 0,
  width: "100%",
  [theme.breakpoints.up("sm")]: {
    marginLeft: theme.spacing(3),
    width: "auto",
  },
}));

const SearchIconWrapper = styled("div")(({ theme }) => ({
  padding: theme.spacing(0, 2),
  height: "100%",
  position: "absolute",
  pointerEvents: "none",
  display: "flex",
  alignItems: "center",
  justifyContent: "center",
}));

const StyledInputBase = styled(InputBase)(({ theme }) => ({
  width: "100%",
  height: "100%",
  color: "inherit",
  "& .MuiInputBase-input": {
    height: "100%",
    padding: theme.spacing(1, 1, 1, 0),
    paddingLeft: `calc(1em + ${theme.spacing(4)})`,
    transition: theme.transitions.create("width"),
    width: "100%",
    minWidth: "200px",
    [theme.breakpoints.up("md")]: {
      width: "20ch",
    },
  },
}));

const SearchFilter = ({
  searchTerm,
  setSearchTerm,
  onSearch,
}: SearchFilterProps) => {
  // console.log("SearchFilter mounted with onSearch:", onSearch);

  useEffect(() => {
    const debounced = debounce((value: string) => {
      if (typeof onSearch === "function") {
        if (value.length >= 3) {
          onSearch(value);
        } else {
          onSearch("");
        }
      } else {
        console.warn("onSearch is not a function", onSearch);
      }
    }, 400);

    debounced(searchTerm);

    return () => debounced.cancel();
  }, [searchTerm]);

  return (
    <Box sx={{ flexGrow: 1, height: "100%" }}>
      <SearchContainer>
        <SearchIconWrapper>
          <MdFilterList />
        </SearchIconWrapper>
        <StyledInputBase
          placeholder="Search courses..."
          inputProps={{ "aria-label": "search" }}
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
        />
        {/* Крестик-сброс */}
        {searchTerm.length > 0 && (
          <Box
            onClick={() => {
              setSearchTerm("");
              onSearch("");
            }}
            sx={{
              position: "absolute",
              right: 8,
              top: "60%",
              transform: "translateY(-50%)",
              cursor: "pointer",
              color: "white",
              opacity: 0.7,
              "&:hover": { opacity: 1 },
            }}
          >
            <CloseIcon fontSize="small" />
          </Box>
        )}
      </SearchContainer>
    </Box>
  );
};

export default SearchFilter;
