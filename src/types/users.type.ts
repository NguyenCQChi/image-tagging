import { StatType } from './stats.type';

export type UserType = {
   userName: string,
   email: string,
   token: string,
   num_request: number,
   stat: StatType[]
}