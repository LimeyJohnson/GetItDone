import TS = require("taskschedule");
export class Task {
    CatagoryID: number;
    TaskID: number;
    Name: string;
    Details: string;
    Created: any;
    Due: any;
    Completed: Date;
    Priority: number;
    //How long the task is going to take in min
    Duration: number;
    Status: number;
    EditMode: boolean;
    Schedule: TS.TaskSchedule;
}



