using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

//Handles logic for casting card from your hand and determining costs
public class CastComponent : MonoBehaviour
{
    public void canBeCasted(){
        
        
    }

    public List<List<Rune>> getCostMatrix(){
        List<Rune> cost = transform.GetComponent<CardInfo>().TempInfo.DecodeCost();
        List<List<Rune>> cst = new List<List<Rune>>
        {
            new List<Rune>(),
            new List<Rune>()
        };
        int i=0;
        foreach(List<Rune> list in cst){
            list.Add(cost[i]);
            list.Add(cost[i+1]);
            list.Add(cost[i+2]);
            i=3;
        }
        return cst;
    }

    //Algorithms that finds patterns in matrixes
    //Used for findings patterns of runes on the arena
    //Needs to be checked afterwards

    static long mod = 257; // The modular value
    static long r = 256; // radix
    static long dr = 1; // Highest power for row hashing
    static long dc = 1; // Highest power for col hashing
 
    // func that return a power n under mod m in LogN
    static long power(int a, int n, long m)
    {
        if (n == 0) {
            return 1;
        }
        if (n == 1) {
            return a % m;
        }
        long pow = power(a, n / 2, m);
        if ((n & 1) != 0) {
            return ((a % m) * (pow % m) * (pow % m)) % m;
        }
        else {
            return ((pow % m) * (pow % m)) % m;
        }
    }
 
    // Checks if all values of pattern matches with the text
    static bool check(List<List<Rune> > arn,
                      List<List<Rune> > cst, long r, long c)
    {
        for (long i = 0; i < cst.Count; i++) {
            for (long j = 0; j < cst[0].Count; j++) {
                if (cst[(int)i][(int)j]
                    != arn[(int)i + (int)r]
                          [(int)j + (int)c])
                    return false;
            }
        }
        return true;
    }
 
    // Finds the first hash of first n rows where n is no.
    // of rows in pattern
    static List<long> findHash(List<List<Rune> > mat,
                               long row)
    {
        List<long> hash = new List<long>();
        long col = mat[0].Count;
        for (long i = 0; i < col; i++) {
            long h = 0;
            for (long j = 0; j < row; j++) {
                h = ((h * r) % mod
                     + (int) mat[(int)j][(int)i] % mod)
                    % mod;
            }
            hash.Add(h);
        }
        return hash;
    }
 
    // rolling hash function for columns
    static void colRollingHash(List<List<Rune>> arn,
                               List<long> t_hash, long row,
                               long p_row)
    {
        for (long i = 0; i < t_hash.Count; i++) {
            t_hash[(int)i]
                = (t_hash[(int)i] % mod
                   - (((int) arn[(int)row][(int)i] % mod)
                      * (dr % mod))
                         % mod)
                  % mod;
            t_hash[(int)i]
                = ((t_hash[(int)i] % mod) * (r % mod))
                  % mod;
            t_hash[(int)i]
                = (t_hash[(int)i] % mod
                   + (int) arn[(int)row + (int)p_row][(int)i]
                         % mod)
                  % mod;
        }
    }

    //returns list of coordinates. Cooordinates represent the top
    private static List<Vector2> RabinKarp(List<List<Rune>> arn, List<List<Rune>> cst)
    {
        List<Vector2> results = new List<Vector2>();
        long t_row = arn.Count;
        long t_col = arn[0].Count;
        long p_row = cst.Count;
        long p_col = cst[0].Count;
        dr = power((int)r, (int)p_row - 1, mod);
        dc = power((int)r, (int)p_col - 1, mod);
 
        List<long> t_hash = findHash(
            arn, p_row); // Column hash of p_row rows
        List<long> p_hash = findHash(
            cst, p_row); // Column hash of p_row rows
        long p_val = 0; // Hash of entire pattern matrix
        for (long i = 0; i < p_col; i++) {
            p_val = (p_val * r + p_hash[(int)i]) % mod;
        }
        for (long i = 0; i <= (t_row - p_row); i++) {
            long t_val = 0;
            for (long j = 0; j < p_col; j++) {
                t_val
                    = ((t_val * r) + t_hash[(int)j]) % mod;
            }
            for (long j = 0; j <= (t_col - p_col); j++) {
                if (p_val == t_val) {
                    if (check(arn, cst, i, j)) {
                        results.Add(new Vector2(i, j));
                    }
                }
                // Calculating t_val for next set of columns
                t_val = (t_val % mod
                         - ((t_hash[(int)j] % mod)
                            * (dc % mod))
                               % mod
                         + mod)
                        % mod;
                t_val = (t_val % mod * r % mod) % mod;
                t_val = (t_val % mod + t_hash[(int)j] % mod)
                        % mod;
            }
            if (i < t_row - p_row) {
                // Call this function for hashing from next
                // row
                colRollingHash(arn, t_hash, i, p_row);
            }
        }
        return results;
    }
}
