import { StatType } from './stats.type';

export type UserType = {
   userName: string,
   email: string,
   refreshToken: string,
   totalRequest: number,
   endpointInfo: StatType[]
}