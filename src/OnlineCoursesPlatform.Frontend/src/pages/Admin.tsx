import React, { useState } from 'react';
import { Box, Card, CardContent, Typography, Button, Stack } from '@mui/material';

// Временные данные
const mockTeacherRequests = [
  { id: 1, userName: 'Teacher One', email: 'teacher1@gmail.com' },
  { id: 2, userName: 'Teacher Two', email: 'teacher2@gmail.com' },
  { id: 3, userName: 'Teacher Three', email: 'teacher3@gmail.com' },
];

const Admin = () => {
  const [requests, setRequests] = useState(mockTeacherRequests);

  const handleApprove = (id: number) => {
    // Тут будет запрос на бек
    setRequests((prev) => prev.filter((req) => req.id !== id));
    console.log(`Approved user with id: ${id}`);
  };

  const handleReject = (id: number) => {
    // Тут будет запрос на бек
    setRequests((prev) => prev.filter((req) => req.id !== id));
    console.log(`Rejected user with id: ${id}`);
  };

  return (
    <Box sx={{ p: 3 }}>
      <Typography variant="h4" gutterBottom>Teacher Approvals</Typography>

      <Stack spacing={2}>
        {requests.map((teacher) => (
          <Card key={teacher.id} sx={{ p: 2 }}>
            <CardContent>
              <Typography variant="h6">{teacher.userName}</Typography>
              <Typography variant="body2" color="text.secondary">{teacher.email}</Typography>

              <Box sx={{ mt: 2, display: 'flex', gap: 2 }}>
                <Button variant="contained" color="success" onClick={() => handleApprove(teacher.id)}>
                  Approve
                </Button>
                <Button variant="contained" color="error" onClick={() => handleReject(teacher.id)}>
                  Reject
                </Button>
              </Box>
            </CardContent>
          </Card>
        ))}
      </Stack>

      {requests.length === 0 && (
        <Typography variant="h6" sx={{ mt: 4 }}>
          No pending teacher requests.
        </Typography>
      )}
    </Box>
  );
};

export default Admin;
