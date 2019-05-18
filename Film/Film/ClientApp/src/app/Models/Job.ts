
import { Knowledge } from './knowledge';
import { User } from './user';

export class Job {
  id: string;
  userCreator: User;
  userWorker: User;
  knowledges: Knowledge[];
  //public virtual List < User > UserPreWorker { get; set; }
  //0:sin PreWork ,1:Con Prework, 2: Con UserWorker, 3: Finalizado
  status: number;
  descripton: string;
  tittle: string;
  //public List < JobKnowledges > JobKnowledges { get; set; }
  jobImages: string[];
  createdDate: Date;

};
