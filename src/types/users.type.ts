import { StatType } from './stats.type';

export type UserType = {
   username: string,
   email: string,
   token: string,
   num_request: number,
   stat: StatType[]
}