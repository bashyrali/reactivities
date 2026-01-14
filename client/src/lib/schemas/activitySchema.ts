import { z } from "zod";
import {Activity} from './../types/index'

const requiredString = (fieldName: string) =>
  z.string({ error: `${fieldName} is required` }).min(1, {error: `${fieldName} is required` });

export const activitySchema = z.object({
  tittle: requiredString('Tittle'),
  description: requiredString('Tittle'),
  category: requiredString('Tittle'),
  tittle: requiredString('Tittle'),
  tittle: requiredString('Tittle'),
  tittle: requiredString('Tittle'),

});
export type ActivitySchema = z.infer<typeof activitySchema>;
