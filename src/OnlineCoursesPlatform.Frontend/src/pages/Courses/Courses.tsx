import Box from "@mui/material/Box";
import Grid from "@mui/material/Grid";
import BasicCard from "../../components/common/BasicCard/BasicCard";
import { coursesData } from "./courseData";

export default function FullWidthGrid() {
  return (
     <Box sx={{ flexGrow: 1 }}>
      <Grid container spacing={2}>
        {coursesData.map((course, index) => {
          const size =
            index === 0
              ? { xs: 12, md: 12, lg: 8 }
              : index === 1
              ? { xs: 12, md: 6, lg: 4 }
              : index === 2
              ? { xs: 12, md: 6, lg: 4 }
              : index === 3
              ? { xs: 12, md: 12, lg: 8 }
              : { xs: 12, sm: 12, md: 6, lg: 4 };

          return (
            <Grid key={index} size={size}>
              <BasicCard {...course} />
            </Grid>
          );
        })}
      </Grid>
    </Box>
  );
}
