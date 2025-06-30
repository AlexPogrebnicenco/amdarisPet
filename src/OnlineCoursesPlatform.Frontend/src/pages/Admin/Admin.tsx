import { useEffect, useState } from "react";
import { Box, Divider, Grid, Typography, useTheme } from "@mui/material";
import InfiniteScroll from "react-infinite-scroll-component";
import { AnimatePresence, motion } from "framer-motion";
import TeacherRequestCard from "../../components/common/TeacherRequestCard/TeacherRequestCard";
import { getPendingTeacherRequests, updateTeacherRequestStatus, type TeacherRequestDto } from "../../services/teacherRequestService";

const PAGE_SIZE = 10;

const Admin = () => {
  const [requests, setRequests] = useState<TeacherRequestDto[]>([]);
  const [page, setPage] = useState(1);
  const [hasMore, setHasMore] = useState(true);
  const theme = useTheme();

  useEffect(() => {
    loadRequests();
  }, []);

  const loadRequests = async () => {
    try {
      const response = await getPendingTeacherRequests(page, PAGE_SIZE);
      setRequests((prev) => [...prev, ...response.items]);

      if (requests.length + response.items.length >= response.totalCount) {
        setHasMore(false);
      } else {
        setPage((prev) => prev + 1);
      }
    } catch (error) {
      console.error("Error fetching teacher requests", error);
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
      <Typography variant="h4" gutterBottom sx={{ color: theme.palette.text.secondary }}>
        Teacher Approvals
      </Typography>
      <Divider sx={{ mb: 2 }} />

      <InfiniteScroll
        dataLength={requests.length}
        next={loadRequests}
        hasMore={hasMore}
        loader={<h4>Loading...</h4>}
        endMessage={<p style={{ textAlign: "center" }}><b>All requests loaded.</b></p>}
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

      {requests.length === 0 && (
        <Typography variant="h6" sx={{ mt: 4 }}>
          No pending teacher requests.
        </Typography>
      )}
    </Box>
  );
};

export default Admin;
