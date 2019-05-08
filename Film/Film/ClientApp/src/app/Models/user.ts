import { UserDates } from './userDates';
import { Knowledge } from './knowledge';

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
};
