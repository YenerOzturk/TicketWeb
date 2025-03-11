DECLARE @CatalogName NVARCHAR(50)
DECLARE @BozukAreaProductsQuery NVARCHAR(4000)
CREATE TABLE #BozukAreaProductsTemp (CatalogName NVARCHAR(50), CategoryName NVARCHAR(50),DisplayName NVARCHAR(500), DefinitionName NVARCHAR(50), RestaurantArea NVARCHAR(50))
	--Catalogları gezecek Cursor tanýmlýyoruz
	DECLARE BozukAreaImageCatalogCursor CURSOR FAST_FORWARD
		FOR SELECT DISTINCT CatalogName	FROM [YemekSepeti_productcatalog].dbo.Cities WITH(NOLOCK)
			WHERE CatalogName NOT IN ('Market','postakip','Irmik','LOKUM') AND isActive = 1
	OPEN BozukAreaImageCatalogCursor
	FETCH NEXT FROM BozukAreaImageCatalogCursor INTO @CatalogName
	WHILE @@FETCH_STATUS = 0
	BEGIN
		PRINT @CatalogName
		SET @BozukAreaProductsQuery = N'
		INSERT INTO #BozukAreaProductsTemp
		select  CatalogName,CategoryName, DisplayName, DefinitionName, RestaurantArea from ['+@CatalogName+'_tr-TR] NOLOCK
where DefinitionName = ''Restaurant'' and RestaurantArea not in 
(
select Id from YemekSepeti_productcatalog..Areas 
)
		';
		EXEC (@BozukAreaProductsQuery)
		FETCH NEXT FROM BozukAreaImageCatalogCursor INTO @CatalogName
	END
	CLOSE BozukAreaImageCatalogCursor
	DEALLOCATE BozukAreaImageCatalogCursor
	SELECT temp1.catalogname,temp1.DisplayName,temp1.categoryname,temp1.RestaurantArea,temp2.TargetAreaId FROM #BozukAreaProductsTemp  temp1 WITH(NOLOCK)
    inner join YemekSepeti_productcatalog..AreaDeleteRequest temp2 on temp1.RestaurantArea = temp2.SourceAreaId

	

	DECLARE @Catalog nvarchar(50)
	DECLARE @Area nvarchar(50)
	DECLARE @Target nvarchar(50)

	DECLARE @UpdateSql nvarchar(max)

	DECLARE UpdateCursor CURSOR FAST_FORWARD
	FOR 
	SELECT temp1.catalogname,temp1.RestaurantArea,temp2.TargetAreaId FROM #BozukAreaProductsTemp  temp1 WITH(NOLOCK)
    inner join YemekSepeti_productcatalog..AreaDeleteRequest temp2 on temp1.RestaurantArea = temp2.SourceAreaId
	

	OPEN UpdateCursor

	FETCH NEXT FROM UpdateCursor INTO @Catalog,@Area,@Target
	WHILE @@FETCH_STATUS = 0
	BEGIN

	SET @UpdateSql='UPDATE ['+@Catalog+'_CatalogProducts]  SET RestaurantArea='''+@Target+''' WHERE RestaurantArea='''+ @Area+'''';

	PRINT @UpdateSql

	EXEC (@UpdateSql)

	FETCH NEXT FROM UpdateCursor INTO @Catalog,@Area,@Target
	END
	CLOSE UpdateCursor
	DEALLOCATE UpdateCursor
	


	DROP TABLE #BozukAreaProductsTemp


