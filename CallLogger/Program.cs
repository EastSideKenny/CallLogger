using CallLogger;

Logger log = new ();
log.startDay();
Call call = new("Jempy", "Kallef", "Test", 14111, Status.Open);
Call call2 = new("Jempsssy", "Kallef", "Test", 14111, Status.Open);
log.AddCall(call);
log.AddCall(call2);

Console.WriteLine(log.CallList[0].Caller);
Console.WriteLine(log.CallList[1].Caller);
Console.WriteLine(log.CallList[0].Date);

log.Save();
log.Load("2022-05-03");

Console.WriteLine(log.CallList[0].Caller);
Console.WriteLine(log.CallList[1].Caller);
Console.WriteLine(log.CallList[0].Date);

Call call3 = new("Je", "Kallef", "Test", 14121, Status.Resolved);
log.AddCall(call3);

log.Save();

//log.endDay();
//Console.WriteLine(log.CallList.Count());