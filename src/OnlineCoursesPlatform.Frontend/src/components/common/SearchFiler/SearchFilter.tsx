import { styled, alpha } from "@mui/material/styles";
import InputBase from "@mui/material/InputBase";
import Box from "@mui/material/Box";
import { useState } from "react";
import useMediaQuery from "@mui/material/useMediaQuery";
import { useTheme } from "@mui/material/styles";
import { IconButton } from "@mui/material";
import { MdFilterList } from "react-icons/md";

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

const SearchFilter = () => {
  const theme = useTheme();

  return (
    <Box sx={{ flexGrow: 1, height: "100%" }}>
      <SearchContainer>
        <SearchIconWrapper>
          <MdFilterList />
        </SearchIconWrapper>
        <StyledInputBase
          placeholder="Filter 80 results..."
          inputProps={{ "aria-label": "search" }}
        />
      </SearchContainer>
    </Box>
  );
};

export default SearchFilter;
