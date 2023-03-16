import { Member } from "./member";

export interface course {
  courseID: number;
  courseName: string;
  courseDescription: string;
  courseStatus: boolean;
  teacherID: number;
  teacherName: string;
  photoUrl: string;
  studentsCount: number;
  students: Member[];
}

