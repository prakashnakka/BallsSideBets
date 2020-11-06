CREATE TRIGGER [dbo].[PlayerPicks_On_Insert_Trig] ON [dbo].[playerpicks]
FOR INSERT
AS

INSERT INTO playerpicks_audit(
		combinedscore, 
		firstinningwickets, 
		secondinningwickets, 
		highestscore, 
		highestwickets, 
		overschase, 
		total4s, 
		total6s, 
		Teampick,
		MaxSingleOverScore,
		userId,
		addDt)
    SELECT
        combinedscore, 
		firstinningwickets, 
		secondinningwickets, 
		highestscore, 
		highestwickets, 
		overschase, 
		total4s, 
		total6s, 
		Teampick,
		MaxSingleOverScore,
		userId,
		GetDate()
        FROM inserted
go