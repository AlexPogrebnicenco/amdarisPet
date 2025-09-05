export interface CreateCourseRequest {
  title: string;
  description: string;
  categoryId: number;
  tagIds: number[];
  difficulty: "Beginner" | "Intermediate" | "Advanced"; 
  about?: string | null;
}

export interface TeacherOwnCourse {
  id: number;
  title: string;
  description: string;
  dateCreated: string;
  dateModified: string;
   difficulty: "Beginner" | "Intermediate" | "Advanced";
  categoryName: string;
  lessonsCount: number;
  authorAvatarUrl: string | null;
  authorId: number;
  about?: string | null;
}

export interface PublicCourse {
  id: number;
  title: string;
  description: string;
  dateCreated: string;
  dateModified: string;
  difficulty: "Beginner" | "Intermediate" | "Advanced";
  categoryName: string;
  lessonsCount: number;
  authorAvatarUrl: string | null;
  authorId: number;
  about?: string | null;
}

export interface CourseDetails {
  id: number;
  title: string;
  description: string;
  categoryId: number | null;
  dateCreated: string;
  dateModified: string;
  authorId: number;
  about?: string | null;
   categoryName: string;
  difficulty: "Beginner" | "Intermediate" | "Advanced";
}