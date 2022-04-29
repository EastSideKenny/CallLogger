using CallLogger;

Logger log = new ();
log.startDay();
Call call = new("Jempy", "Kallef", "Test", 14111, Status.Open);
Call call2 = new("Jempsssy", "Kallef", "Test", 14111, Status.Open);
log.AddCall(call);
log.AddCall(call2);

Console.WriteLine(log.CallList[0].Caller);
Console.WriteLine(log.CallList[1].Caller);