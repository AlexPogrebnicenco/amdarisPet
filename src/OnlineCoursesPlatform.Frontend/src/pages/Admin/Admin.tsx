import { useEffect, useState } from "react";
import {
  Box,
  CircularProgress,
  Divider,
  Grid,
  Typography,
  useTheme,
} from "@mui/material";
import InfiniteScroll from "react-infinite-scroll-component";
import { AnimatePresence, motion } from "framer-motion";
import TeacherRequestCard from "../../components/common/TeacherRequestCard/TeacherRequestCard";
import {
  getPendingTeacherRequests,
  updateTeacherRequestStatus,
  type TeacherRequestDto,
} from "../../services/teacherRequestService";

const PAGE_SIZE = 10;

const Admin = () => {
  const [requests, setRequests] = useState<TeacherRequestDto[]>([]);
  const [page, setPage] = useState(1);
  const [hasMore, setHasMore] = useState(true);
  const [isLoadingFirstPage, setIsLoadingFirstPage] = useState(true);
  const theme = useTheme();

  useEffect(() => {
    loadRequests();
  }, []);

  const loadRequests = async () => {
    try {
      // await new Promise((res) => setTimeout(res, 5000));
      const response = await getPendingTeacherRequests(page, PAGE_SIZE);
      setRequests((prev) => [...prev, ...response.items]);

      if (requests.length + response.items.length >= response.totalCount) {
        setHasMore(false);
      } else {
        setPage((prev) => prev + 1);
      }
    } catch (error) {
      console.error("Error fetching teacher requests", error);
    } finally {
      setIsLoadingFirstPage(false);
    }
  };

  const handleApprove = async (id: number) => {
    try {
      await updateTeacherRequestStatus(id, "Approved");
      setRequests((prev) => prev.filter((req) => req.id !== id));
    } catch (error) {
      console.error("Error approving teacher request", error);
    }
  };

  const handleReject = async (id: number) => {
    try {
      await updateTeacherRequestStatus(id, "Rejected");
      setRequests((prev) => prev.filter((req) => req.id !== id));
    } catch (error) {
      console.error("Error rejecting teacher request", error);
    }
  };

  return (
    <Box>
      <Typography
        variant="h4"
        gutterBottom
        sx={{ color: theme.palette.text.secondary }}
      >
        Teacher Approvals
      </Typography>
      <Divider sx={{ mb: 2 }} />

      <InfiniteScroll
        dataLength={requests.length}
        next={loadRequests}
        hasMore={hasMore}
        loader={
          <Box sx={{ display: "flex", justifyContent: "center", my: 4 }}>
            <CircularProgress />
          </Box>
        }
        endMessage={
          requests.length > 0 ? (
            <Typography
              sx={{
                textAlign: "center",
                my: 4,
                color: theme.palette.text.secondary,
              }}
            >
              <b>All requests loaded.</b>
            </Typography>
          ) : null
        }
      >
        <Grid container spacing={2}>
          <AnimatePresence>
            {requests.map((teacher) => (
              <Grid key={teacher.id} size={{ xs: 12, sm: 12, md: 6, lg: 4 }}>
                <motion.div
                  initial={{ opacity: 0, scale: 0.9 }}
                  animate={{ opacity: 1, scale: 1 }}
                  exit={{ opacity: 0, scale: 0.9 }}
                  transition={{ duration: 0.3 }}
                >
                  <TeacherRequestCard
                    userName={teacher.userName}
                    email={teacher.email}
                    requestDate={teacher.requestedAt}
                    onApprove={() => handleApprove(teacher.id)}
                    onReject={() => handleReject(teacher.id)}
                  />
                </motion.div>
              </Grid>
            ))}
          </AnimatePresence>
        </Grid>
      </InfiniteScroll>

      {!isLoadingFirstPage && requests.length === 0 && (
        <Typography variant="h6" sx={{ mt: 4 }} textAlign={"center"} >
          No pending teacher requests.
        </Typography>
      )}
    </Box>
  );
};

export default Admin;
