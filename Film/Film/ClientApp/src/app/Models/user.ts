import { UserDates } from './userDates';
import { Knowledge } from './knowledge';
import { Notification } from './Notification';
export class  User {
  admin: string;
  email: string;
  emailConfirmed: boolean;
  userDates: UserDates;
  accessFailedCount: string;
  rememberMe: boolean;
  token: string;
  tokenExpiration: string;
  knowledges: Knowledge[];
  notifications: Notification[]
};
