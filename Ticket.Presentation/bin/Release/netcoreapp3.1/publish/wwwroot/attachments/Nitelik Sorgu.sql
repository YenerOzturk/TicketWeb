


BEGIN TRY
	BEGIN TRAN
		SELECT * INTO mktg_expression_backup FROM mktg_expression
		SELECT * INTO mktg_target_backup FROM mktg_target

		---DELETE 1 Start
		DELETE FROM mktg_expression WHERE i_expr_id IN(

			SELECT DeleteRecords.i_expr_id FROM 
			(
				SELECT i_expr_id FROM mktg_expression A WITH (NOLOCK) 
				WHERE NOT EXISTS 
				( 
				 SELECT * FROM mktg_target B WITH (NOLOCK) WHERE A.i_expr_id=B.i_target_expr_id 
				)
			) As DeleteRecords
			
			LEFT OUTER JOIN KampusExpression C ON (DeleteRecords.i_expr_id=C.i_expr_id)
			
			LEFT OUTER JOIN mktg_expression_depends D ON (DeleteRecords.i_expr_id=D.i_expr_id)
			
			LEFT OUTER JOIN mktg_order_discount_condition F ON (DeleteRecords.i_expr_id=F.i_cond_expr)
			
			LEFT OUTER JOIN mktg_order_discount_expression G ON (DeleteRecords.i_expr_id=G.i_discexpr_expr)

			LEFT OUTER JOIN  dbo.mktg_order_discount H ON (DeleteRecords.i_expr_id=H.i_disc_award_expr)

			WHERE 
			
			C.i_expr_id is null AND D.i_expr_id is null AND F.i_cond_id is null AND G.i_discexpr_expr is null AND H.i_disc_award_expr is null 
		
		)
		---DELETE 1 END

		---DELETE 2 START ---- Bu sorguda kampanyasi silinmis ve target tablosunda kaydi kalmis datalari bulup expression tablosundan siliyoruz.
		DELETE FROM mktg_expression WHERE i_expr_id IN 
		(
		
		SELECT A.i_expr_id FROM mktg_expression A WITH (NOLOCK) 
		
		LEFT OUTER JOIN KampusExpression C ON (A.i_expr_id=C.i_expr_id)
		
		LEFT OUTER JOIN mktg_expression_depends D ON (A.i_expr_id=D.i_expr_id)
		
		LEFT OUTER JOIN mktg_order_discount_condition F ON (A.i_expr_id=F.i_cond_expr)
		
		LEFT OUTER JOIN mktg_order_discount_expression G ON (A.i_expr_id=G.i_discexpr_expr)

		LEFT OUTER JOIN  dbo.mktg_order_discount H ON (A.i_expr_id=H.i_disc_award_expr)

		WHERE EXISTS (
						SELECT i_target_expr_id FROM mktg_target B WITH (NOLOCK) WHERE 
						not exists (SELECT * FROM mktg_campaign_item WITH (NOLOCK) WHERE i_campitem_id=B.i_campitem_id)
						AND A.i_expr_id=B.i_target_expr_id
						
					)  AND C.i_expr_id is null AND D.i_expr_id is null AND F.i_cond_id is null AND G.i_discexpr_expr is null AND H.i_disc_award_expr is null 
		)
		---DELETE 2 END

		---DELETE 3 START ---- Bu sorguda kampanyasi silinmis ve target tablosunda kaydi kalmis datalari bulup siliyoruz.
		DELETE FROM mktg_target WHERE i_target_expr_id IN (
		
		SELECT i_expr_id FROM mktg_expression A WITH (NOLOCK) 
		
		WHERE EXISTS (
				SELECT i_target_expr_id FROM mktg_target B WITH (NOLOCK) WHERE 
				not exists (SELECT * FROM mktg_campaign_item WITH (NOLOCK) WHERE i_campitem_id=B.i_campitem_id)
				AND A.i_expr_id=B.i_target_expr_id
			) 

			--AND u_expr_category= N'TargetCondition'
		)
		---DELETE 3 END


	COMMIT TRAN
END TRY
BEGIN CATCH
	ROLLBACK TRAN
END CATCH
